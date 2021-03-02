using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CharacterMovement : NetworkBehaviour
{
    public CharacterController controller;
    public float baseMovementSpeed = 1;

    public float walkSpeed;
    public float runSpeed;

    public float lerpSpeed = 1;
    public bool isWalking;

    public Animator animator;
    public float newSpeed;
    
    public Vector3 currentMovement = Vector3.zero;
    public Vector3 absoluteMovement = Vector3.zero;
    
    // Mouse looking target
    public Transform target;

    public Transform camera;
    public Transform cameraTarget;
    
    public bool blocked;

    private void Start()
    {
        camera = Camera.main.transform;
        animator = GetComponent<Animator>();
        
        if(!isLocalPlayer) return;
        
        CameraManager.singleton.SetCameraFollowTarget(cameraTarget);
    }

    private void LateUpdate()
    {
        if(!isLocalPlayer) return;
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isWalking = !isWalking; 
        }
    }

    private void Update()
    {
        if(!isLocalPlayer) return;
        
        // Input WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Joins movement input
        Vector2 newMovementInput = new Vector3(horizontal, vertical) * (isWalking ? walkSpeed : runSpeed);
        
        // Interpolates current movement to new movement
        currentMovement = Vector3.Lerp(currentMovement, newMovementInput * (baseMovementSpeed), Time.deltaTime * (lerpSpeed * 2));

        float currentAnimatorSpeed = animator.GetFloat("Speed");
        animator.speed = baseMovementSpeed;

        // if there's no input we interpolate to zero
        if (newMovementInput.magnitude == 0)
        {
            currentMovement = Vector3.Lerp(currentMovement, Vector3.zero, Time.deltaTime * 4);
            
            // if there's no movement we make the animator speed variable go to zero
            newSpeed = Mathf.LerpUnclamped(currentAnimatorSpeed, 0 , Time.deltaTime * 30);    
        }
        // If there's movement we update the animator speed variable
        else
        {
            newSpeed = Mathf.Lerp(currentAnimatorSpeed, isWalking ? .3f : 1 , Time.deltaTime * 2);            
        }

        // This makes sure your input is based on your camera, so when you press W while looking forward its not shit
        // I have no idea how it really works
        Vector3 finalMovement =
            currentMovement.y * Vector3.Cross(camera.right, Vector3.up).normalized +
            currentMovement.x * Vector3.Cross(Vector3.up, camera.forward).normalized;

        // Finally, we do a final lerp to make sure you won't just rotate freely the player when rotating the camera
        if (newMovementInput.magnitude > 0)
            absoluteMovement = Vector3.Lerp(absoluteMovement, finalMovement, Time.deltaTime * 2);
        else
            absoluteMovement = Vector3.Lerp(absoluteMovement, Vector3.zero, Time.deltaTime * 5);
        
        // restrict the movement if we have blocked the character
        if (!blocked)
        {
            // Actually move
            controller.Move((absoluteMovement) * Time.deltaTime);
            // Look at target
            transform.rotation = Quaternion.LookRotation(absoluteMovement + (transform.forward * 10));
            // Change animator speed value
            animator.SetFloat("Speed", newSpeed);
        }
    }
}
