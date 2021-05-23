using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Random = UnityEngine.Random;

public class LocalPlayerManager : MonoBehaviour
{
    public static LocalPlayerManager singleton { get; private set; }

    public NetworkConnection networkConnection;
    public Soul soul;

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
    }

    public Vector3 GetSoulEntityPosition(bool offset = false)
    {
        Transform entity = soul.entity.transform;
        
        Vector3 heightOffset = new Vector3(0, 1, 0);
        Vector3 position = entity.position + heightOffset;

        float offsetPos = Random.Range(.1f, 1.1f); 
        Vector3 forward = entity.forward * offsetPos;
        
        return offset ? position + forward : position;
    }
}
