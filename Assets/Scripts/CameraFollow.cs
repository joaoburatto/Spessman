using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform camera;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    
    private void Update()
    {
        // Set camera to the target position 
        transform.position = target.position - positionOffset;
        
        // Rotation
        Vector3 newRotation = rotationOffset;

        // Mouse input for rotation
        Vector3 input = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        newRotation += input;

        // Mouse input for zoom
        float zoom = Input.GetAxis("Mouse ScrollWheel");
        camera.position += camera.forward * zoom;
        
        // Set camera rotation
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + newRotation);
    }
}
