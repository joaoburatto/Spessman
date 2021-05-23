using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera camera;

    public Transform origin;
    public float heightOffset;

    public float goingSpeed = 1;
    public float retrieveSpeed = 1;
    public float forwardMultiplier = 1;
    
    public bool blocked;

    public bool spaceForForward;
    
    public Vector3 currentTarget;
    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (currentTarget.magnitude == 0)
        {
            MoveTargetToOrigin();
        }
        else
        {
            MoveTargetToPosition();
        }
        
        if (spaceForForward && Input.GetKey(KeyCode.Space))
        {
            MoveTargetToForward();
        }
    }

    public void MoveTargetToMouse()
    {
        mousePos = Vector3.LerpUnclamped(mousePos, GetMousePosition(true), Time.deltaTime * goingSpeed);
        transform.position = mousePos;
    }

    public void MoveTargetToOrigin()
    {
        mousePos = Vector3.Lerp(mousePos, origin.position + (origin.forward * forwardMultiplier) + origin.up * heightOffset, Time.deltaTime * retrieveSpeed);
        transform.position = mousePos;
    }
    
    public void MoveTargetToForward()
    {
        mousePos = Vector3.Lerp(mousePos, origin.position + origin.up * heightOffset, Time.deltaTime * retrieveSpeed);
        transform.position = mousePos;
    }
    
    public void MoveTargetToPosition()
    {
        mousePos = Vector3.Lerp(mousePos, currentTarget, Time.deltaTime * retrieveSpeed);
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
