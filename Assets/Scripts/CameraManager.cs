using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager singleton { get; private set; }

    public Camera lobbyCamera;
    
    public Camera playerCamera;
    public CameraFollow cameraFollow;

    public void SetCameraFollowTarget(Transform target)
    {
        cameraFollow.SetTarget(target);
    }
    private void Awake()
    {
        if (singleton != null) Destroy(gameObject);
        singleton = this;
    }
}