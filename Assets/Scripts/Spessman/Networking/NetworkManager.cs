using System;
using Mirror;
using Spessman.Entities;
using TMPro;
using UnityEngine;

namespace Spessman.Networking
{
    public class NetworkManager : Mirror.NetworkManager
    {
        public static NetworkManager singleton { get; private set; }
        public GameObject humanPrefab;

        public override void Awake()
        {
            base.Awake();
            InitializeSingleton();
        }
        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Transform startPos = GetStartPosition();
            GameObject player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);
            
            NetworkServer.AddPlayerForConnection(conn, player);
        }
        
        public void SpawnPlayer(NetworkConnection conn)
        {
            Debug.Log("Spawning player, " + "conn: " + conn.address);

            // Spawn player based on their character choices
            Transform startPos = GetStartPosition();

            GameObject player = startPos != null
                ? Instantiate(humanPrefab, startPos.position, startPos.rotation)
                : Instantiate(humanPrefab);
            
            //Spawn actual player
            NetworkServer.ReplacePlayerForConnection(conn, player);

            Soul soul = LocalPlayerManager.singleton.soul;
            
            soul.entity = player.GetComponent<Entity>();
        }

        bool InitializeSingleton()
        {
            if (singleton != null && singleton == this) return true;

            if (dontDestroyOnLoad)
            {
                if (singleton != null)
                {
                    Destroy(gameObject);

                    // Return false to not allow collision-destroyed second instance to continue.
                    return false;
                }

                singleton = this;
                if (Application.isPlaying) DontDestroyOnLoad(gameObject);
            }
            else
            {
                singleton = this;
            }
            return true;
        }
    }
}