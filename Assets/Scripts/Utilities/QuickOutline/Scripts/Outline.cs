//
//  Outline.cs
//  QuickOutline
//
//  Created by Chris Nolet on 3/30/18.
//  Copyright © 2018 Chris Nolet. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities.QuickOutline.Scripts
{
    /// <summary>
    /// This script helps us add an Outline to stuff.
    ///
    /// Because of how it works, we need one material per object to make sure we don't fuck it up
    /// </summary>
    [DisallowMultipleComponent]
    public class Outline : MonoBehaviour
    {
        private static HashSet<Mesh> registeredMeshes = new HashSet<Mesh>();

        public enum Mode
        {
            OutlineAll,
            OutlineVisible,
            OutlineHidden,
            OutlineAndSilhouette,
            SilhouetteOnly
        }

        public Mode OutlineMode
        {
            get { return outlineMode; }
            set
            {
                outlineMode = value;
                needsUpdate = true;
            }
        }

        public Color OutlineColor
        {
            get { return outlineColor; }
            set
            {
                outlineColor = value;
                needsUpdate = true;
            }
        }

        public float OutlineWidth
        {
            get { return outlineWidth; }
            set
            {
                outlineWidth = value;
                needsUpdate = true;
            }
        }

        [Serializable]
        private class ListVector3
        {
            public List<Vector3> data;
        }

        [SerializeField] private Mode outlineMode;

        [SerializeField] private Color outlineColor = Color.white;

        [SerializeField, Range(0f, 50f)] private float outlineWidth = 2f;

        [Header("Optional")]
        [SerializeField, Tooltip(
             "Precompute enabled: Per-vertex calculations are performed in the editor and serialized with the object. "
             + "Precompute disabled: Per-vertex calculations are performed at runtime in Awake(). This may cause a pause for large meshes.")]
        private bool precomputeOutline;

        [SerializeField, HideInInspector] private List<Mesh> bakeKeys = new List<Mesh>();

        [SerializeField, HideInInspector] private List<ListVector3> bakeValues = new List<ListVector3>();

        [SerializeField] private Renderer[] renderers;

        private Material outlineMaskMaterial;
        private Material outlineFillMaterial;

        private bool needsUpdate;

        public bool permanent;

        public bool getChildren;

        [Tooltip("Makes outline follow normal")]
        public bool normal;

        void Awake()
        {
            // Get renderers
            SetupRenderers();

            // Loads up the materials we need
            SetupMaterials();

            // To Retrieve or generate smooth normals
            LoadSmoothNormals();

            // Apply material properties immediately
            needsUpdate = true;
        }

        // loads up the materials we need
        void SetupMaterials()
        {
            // Instantiate outline materials
            outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));

            if (normal)
            {
                outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill Normal"));
                outlineFillMaterial.name = "OutlineFill Normal (Instance)";
            }
            else
            {
                outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));
                outlineFillMaterial.name = "OutlineFill (Instance)";
            }

            outlineMaskMaterial.name = "OutlineMask (Instance)";
        }

        // sets up the renderers so everything is outlined correctly
        void SetupRenderers()
        {
            List<Renderer> renderersList = new List<Renderer>();

            // if there is a renderer on the root we add it to the renderer list
            if (GetComponent<Renderer>() != null)
                renderersList.Add(GetComponent<Renderer>());

            // then we add the renderers from the childs
            Renderer[] childRenderersArray = GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in childRenderersArray)
                if (renderer != null) renderersList.Add(renderer);

        
            // then we update the renderer list
            renderers = renderersList.ToArray();
        }

        // Adds the Mask and Fill outline materials into all the renderers in the object
        [ContextMenu("Add Materials")]
        public void AddMaterials()
        {
            // to avoid problems we call them again, even if they are expensive
            SetupRenderers();
            SetupMaterials();

            foreach (Renderer renderer in renderers)
            {
                // Append outline shaders
                List<Material> materials = renderer.sharedMaterials.ToList();

                // adds the outline materials to the temporary renderer list
                materials.Add(outlineMaskMaterial);
                materials.Add(outlineFillMaterial);

                // makes sure the material properties are the same as in the component
                UpdateMaterialProperties();

                // sets up the renderer materials
                renderer.materials = materials.ToArray();
            }
        }
    
        // Removes the Mask and Fill outline materials into all the renderers in the object
        [ContextMenu("Remove Materials")]
        public void RemoveMaterials()
        {
            foreach (Renderer renderer in renderers)
            {
                // Remove outline shaders
                List<Material> materials = renderer.sharedMaterials.ToList();

                // check for the materials and removes them
                if (materials.Exists(material => outlineMaskMaterial))
                    materials.Remove(outlineMaskMaterial);
                if (materials.Exists(material => outlineFillMaterial))
                    materials.Remove(outlineFillMaterial);

                // unloads the material
                Destroy(outlineMaskMaterial);
                Destroy(outlineFillMaterial);
            
                // sets the materials back in the original state
                renderer.materials = materials.ToArray();
            }
        }

        void OnValidate()
        {
            // Update material properties
            needsUpdate = true;

            // Clear cache when baking is disabled or corrupted
            if (!precomputeOutline && bakeKeys.Count != 0 || bakeKeys.Count != bakeValues.Count)
            {
                bakeKeys.Clear();
                bakeValues.Clear();
            }

            // Generate smooth normals when baking is enabled
            if (precomputeOutline && bakeKeys.Count == 0)
            {
                Bake();
            }
        }

        void Update()
        {
            if (needsUpdate)
            {
                needsUpdate = false;

                UpdateMaterialProperties();
            }
        }
    
        void OnDestroy()
        {
            // Destroy material instances
            Destroy(outlineMaskMaterial);
            Destroy(outlineFillMaterial);
        }

        void Bake()
        {
            // Generate smooth normals for each mesh
            var bakedMeshes = new HashSet<Mesh>();

            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                // Skip duplicates
                if (!bakedMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                // Serialize smooth normals
                var smoothNormals = SmoothNormals(meshFilter.sharedMesh);

                bakeKeys.Add(meshFilter.sharedMesh);
                bakeValues.Add(new ListVector3() {data = smoothNormals});
            }
        }

        void LoadSmoothNormals()
        {
            // Retrieve or generate smooth normals
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>())
            {
                // Skip if smooth normals have already been adopted
                if (!registeredMeshes.Add(meshFilter.sharedMesh))
                {
                    continue;
                }

                // Retrieve or generate smooth normals
                var index = bakeKeys.IndexOf(meshFilter.sharedMesh);
                var smoothNormals = (index >= 0) ? bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                // Store smooth normals in UV3
                meshFilter.sharedMesh.SetUVs(3, smoothNormals);
            }

            // Clear UV3 on skinned mesh renderers
            foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (registeredMeshes.Add(skinnedMeshRenderer.sharedMesh))
                {
                    skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
                }
            }
        }

        List<Vector3> SmoothNormals(Mesh mesh)
        {
            // Group vertices by location
            var groups = mesh.vertices.Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);

            // Copy normals to a new list
            var smoothNormals = new List<Vector3>(mesh.normals);

            // Average normals for grouped vertices
            foreach (var group in groups)
            {
                // Skip single vertices
                if (group.Count() == 1)
                {
                    continue;
                }

                // Calculate the average normal
                var smoothNormal = Vector3.zero;

                foreach (var pair in group)
                {
                    smoothNormal += mesh.normals[pair.Value];
                }

                smoothNormal.Normalize();

                // Assign smooth normal to each vertex
                foreach (var pair in group)
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }

            return smoothNormals;
        }

        void UpdateMaterialProperties()
        {
            // Apply properties according to mode
            outlineFillMaterial.SetColor("_OutlineColor", outlineColor);

            switch (outlineMode)
            {
                case Mode.OutlineAll:
                    outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                    break;

                case Mode.OutlineVisible:
                    outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                    break;

                case Mode.OutlineHidden:
                    outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Greater);
                    outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                    break;

                case Mode.OutlineAndSilhouette:
                    outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Always);
                    outlineFillMaterial.SetFloat("_OutlineWidth", outlineWidth);
                    break;

                case Mode.SilhouetteOnly:
                    outlineMaskMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    outlineFillMaterial.SetFloat("_ZTest", (float) UnityEngine.Rendering.CompareFunction.Greater);
                    outlineFillMaterial.SetFloat("_OutlineWidth", 0);
                    break;
            }
        }
    }
}