using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;
using NetworkManager = Spessman.Networking.NetworkManager;

public class ServerLobbyUIHelper : NetworkBehaviour
{
    public Button spawnButton;

    public Animator animator;

    private void Start()
    {
        spawnButton.onClick.AddListener( delegate 
        { 
            CmdSpawnPlayer(); 
        });
    }

    public void FadeUI(bool state)
    {
        animator.SetBool("Fade", state);
    }
    [Command(ignoreAuthority = true)]
    public void CmdSpawnPlayer(NetworkConnectionToClient sender = null)
    {
        NetworkManager.singleton.SpawnPlayer(sender);
    }
}
