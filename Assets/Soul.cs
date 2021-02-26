using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Soul : NetworkBehaviour
{
    // Current entity controlled by the Soul
    private Transform entity;
    private void Start()
    {
        if(!isLocalPlayer) return;

        LocalPlayerManager.singleton.networkConnection = netIdentity.connectionToClient;
    }

    public void SetEntity(Transform entity)
    {
        this.entity = entity;
    }
}
