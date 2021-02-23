using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed;
    
    public Vector3 currentMovement = Vector3.zero;

    // Mouse looking target
    public Transform target;

    private void Update()
    {
        // Input WASD
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Joins movement input
        Vector3 newMovementInput = new Vector3(horizontal, 0, vertical);
        
        // Interpolates current movement to new movement
        currentMovement = Vector3.Lerp(currentMovement, newMovementInput * movementSpeed, Time.deltaTime);

        // if there's no input we interpolate to zero
        if (newMovementInput.magnitude == 0)
        {
            currentMovement = Vector3.Lerp(currentMovement, Vector3.zero, Time.deltaTime * 4);
        }

        // Actually move
        controller.Move(currentMovement);
        // Look at target
        transform.LookAt(transform.position + currentMovement);
    }
}
