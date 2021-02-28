using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UIElements;

public class RagdollManager : NetworkBehaviour
{
    public bool ragdoll;
    
    public CharacterMovement characterMovement;

    public Rigidbody[] rigidbodies;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
    }

    public void SetupNetworkTransformChild()
    {
        foreach (Rigidbody rigidBody in rigidbodies)
        {
            NetworkTransformChild networkTransformChild = gameObject.AddComponent<NetworkTransformChild>();
            networkTransformChild.target = rigidBody.transform;
            networkTransformChild.clientAuthority = true;
        }
    }
    
    public void Update()
    {
        if (!isLocalPlayer) return;
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            CmdToggle();
        }

        if (ragdoll)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 force = characterMovement.absoluteMovement;

                foreach (Rigidbody rigidbody in rigidbodies)
                    rigidbody.AddForce(force * 2, ForceMode.Impulse);
            }
        }
    }

    [Command(ignoreAuthority = true)]
    public void CmdToggle()
    {
        ragdoll = !ragdoll;
        
        if (!ragdoll)
        {
            characterMovement.transform.position = rigidbodies[0].position;
        }
        
        characterMovement.blocked = ragdoll;
        characterMovement.controller.enabled = !ragdoll;
        characterMovement.animator.enabled = !ragdoll;

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = !ragdoll;
        
        if (ragdoll)
        {
            Vector3 force = characterMovement.absoluteMovement;

            foreach (Rigidbody rigidbody in rigidbodies)
                rigidbody.AddForce(force * 2.7f, ForceMode.Impulse);
        }

        RpcToggle();
    }
    
    [ClientRpc]
    public void RpcToggle()
    {
        if (isServer) return;
        
        ragdoll = !ragdoll;
        
        if (!ragdoll)
        {
            characterMovement.transform.position = rigidbodies[0].position;
        }
        
        characterMovement.blocked = ragdoll;
        characterMovement.controller.enabled = !ragdoll;
        characterMovement.animator.enabled = !ragdoll;

        foreach (Rigidbody rigidbody in rigidbodies)
            rigidbody.isKinematic = !ragdoll;
        
        if (ragdoll)
        {
            Vector3 force = characterMovement.absoluteMovement;

            foreach (Rigidbody rigidbody in rigidbodies)
                rigidbody.AddForce(force * 2.7f, ForceMode.Impulse);
        }
    }
}
