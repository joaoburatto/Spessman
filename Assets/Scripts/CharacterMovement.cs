using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float movementSpeed;
    
    public Vector3 currentMovement = Vector3.zero;
    
    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 newMovementInput = new Vector3(horizontal, 0, vertical);
        currentMovement = Vector3.Lerp(currentMovement, newMovementInput, Time.deltaTime * movementSpeed);

        controller.Move(currentMovement);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(currentMovement), 360);
    }
}
