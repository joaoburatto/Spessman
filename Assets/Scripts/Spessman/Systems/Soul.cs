using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Spessman;
using Spessman.Entities;

public class Soul : NetworkBehaviour
{
    // Current entity controlled by the Soul
    public Entity entity;
    //
    public string username;
    
    private void Start()
    {
        if(!isLocalPlayer) return;

        LocalPlayerManager.singleton.networkConnection = netIdentity.connectionToClient;
        LocalPlayerManager.singleton.soul = this;
    }

    public void SetEntity(Entity entity)
    {
        this.entity = entity;
    }
}
