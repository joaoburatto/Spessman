using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Spessman;
using UnityEngine;
using UnityEngine.UI;
using NetworkManager = Spessman.Networking.NetworkManager;

public class ServerLobbyUIHelper : NetworkBehaviour
{
    public Button spawnButton;

    public Animator animator;
    
    private NetworkManager networkManager;

    private void Start()
    {
        networkManager = NetworkManager.singleton;
            
        spawnButton.onClick.AddListener( delegate 
        { 
            CmdSpawnPlayer(); 
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { FadeUI();}
    }

    public void FadeUI(bool state)
    {
        animator.SetBool("Fade", state);
    }
    
    public void FadeUI()
    {
        bool state = animator.GetBool("Fade");
        CursorManager.singleton.SetCursorState(!state);
        animator.SetBool("Fade", !state);
    }
    
    [Command(ignoreAuthority = true)]
    public void CmdSpawnPlayer(NetworkConnectionToClient sender = null)
    {
        NetworkManager.singleton.SpawnPlayer(sender);
    }
    
    public void OnDisconnectButtonPressed() 
    {
        if (isServer) networkManager.StopHost();
        if (isClient) networkManager.StopClient();
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
