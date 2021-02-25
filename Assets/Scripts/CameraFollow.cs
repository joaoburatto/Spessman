using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public Transform camera;
    public float zoom;
    
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    public Vector3 cameraPositionOffset;
    public Vector3 cameraRotationOffset;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
    private void Update()
    {
        // Set camera to the target position 
        transform.position = target.position + positionOffset;
        camera.localPosition = cameraPositionOffset;
        
        // Mouse input for rotation
        Vector3 input = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        Vector3 newRotation = input;

        // Mouse input for zoom
        zoom += Input.GetAxis("Mouse ScrollWheel");
        camera.position += camera.forward * zoom;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            float y = Input.GetAxis("Mouse ScrollWheel");
            positionOffset.y += y;
        }
        // Set camera rotation
        camera.localRotation = Quaternion.Euler(cameraRotationOffset);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + newRotation);
    }
}
