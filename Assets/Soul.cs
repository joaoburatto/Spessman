﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Spessman;

public class Soul : NetworkBehaviour
{
    // Current entity controlled by the Soul
    private Entity  entity;
    //
    public string username;
    
    private void Start()
    {
        if(!isLocalPlayer) return;

        LocalPlayerManager.singleton.networkConnection = netIdentity.connectionToClient;
    }

    public void SetEntity(Entity entity)
    {
        this.entity = entity;
    }
}
