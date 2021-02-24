using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera camera;

    public Transform target;
    public float heightOffset;

    public float goingSpeed = 1;
    public float retrieveSpeed = 1;
    
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1)) 
            MoveTargetToMouse();
        else 
            MoveTargetToOrigin();
    }
    
    public void MoveTargetToMouse()
    {
        mousePos = Vector3.Lerp(mousePos, GetMousePosition(true), Time.deltaTime * goingSpeed);
        transform.position = mousePos;
    }

    public void MoveTargetToOrigin()
    {
        mousePos = Vector3.Lerp(mousePos, target.position + (transform.forward * 4) + transform.up * heightOffset, Time.deltaTime * retrieveSpeed);
        transform.position = mousePos;
    }

    public Vector3 GetMousePosition(bool changeYAxis)
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = ray.origin - ray.direction * (ray.origin.y / ray.direction.y);
        mousePos = new Vector3(mousePos.x, changeYAxis ? mousePos.y : transform.position.y, mousePos.z);

        return mousePos;
    }
}
