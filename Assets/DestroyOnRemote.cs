using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class DestroyOnRemote : NetworkBehaviour
{
    public Component[] componentsToDestroy;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            foreach (Component component in componentsToDestroy)
            {
                Destroy(component);
            }
        }
    }
}
