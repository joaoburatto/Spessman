using System;
using System.Collections;
using System.Collections.Generic;
using Spessman;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Transform camera;
    
    public float zoom;
    
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public Vector3 cameraPositionOffset;
    public Vector3 cameraRotationOffset;

    public float sensitivityY;
    public float sensitivityX;
    
    public void SetTarget(Transform target)
    {
        this.target = target;
        CursorManager.singleton.SetCursorBlocked();
    }

    private void Start()
    {
        GeneralSettingsManager settings = GeneralSettingsManager.singleton;
        
        settings.mouseSensitivityXChanged += delegate(float value) { sensitivityX = value; };
        settings.mouseSensitivityYChanged += delegate(float value) { sensitivityY = value; };

        sensitivityX = settings.mouseSensitivityX;
        sensitivityY = settings.mouseSensitivityY;
        
        ConsoleUIHelper.OnConsoleDisabled += delegate { enabled = true; };
        ConsoleUIHelper.OnConsoleEnabled += delegate { enabled = false; };
    }

    private void OnDestroy()
    {
        GeneralSettingsManager settings = GeneralSettingsManager.singleton;
        
        ConsoleUIHelper.OnConsoleDisabled -= delegate { enabled = true; };
        ConsoleUIHelper.OnConsoleEnabled -= delegate { enabled = false; };
        
        settings.mouseSensitivityXChanged -= delegate(float value) { sensitivityX = value; };
        settings.mouseSensitivityYChanged -= delegate(float value) { sensitivityY = value; };
    }

    private void LateUpdate()
    {
        if (target == null)
            return; 
        
        // Set camera to the target position
        // normalize Y so we have noflickering from the animation
        float rootPosition = target.root.position.y;
        Vector3 targetPositionFixed = new Vector3(target.position.x, rootPosition, target.position.z);
        
        Vector3 lerpPos = Vector3.Lerp(transform.position, targetPositionFixed, Time.deltaTime * 10);
        transform.position = target.position + positionOffset;
        camera.localPosition = cameraPositionOffset;
        
        // Mouse input for rotation
        Vector3 input = new Vector3(-Input.GetAxis("Mouse Y") * sensitivityY/10, Input.GetAxis("Mouse X") * sensitivityX/10, 0);
        Vector3 newRotation = input;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            positionOffset.y += Input.GetAxis("Mouse ScrollWheel");
        }
        else
        {
            // Mouse input for zoom
            zoom += Input.GetAxis("Mouse ScrollWheel") * 3;
        }

        camera.position += camera.forward * zoom;
        
        if (Input.GetKey(KeyCode.LeftControl))
        {
            CursorManager.singleton.SetCursorUnlocked();
            return;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            CursorManager.singleton.SetCursorBlocked();
        }
        // Set camera rotation
        camera.localRotation = Quaternion.Euler(cameraRotationOffset);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + newRotation);
    }
}
