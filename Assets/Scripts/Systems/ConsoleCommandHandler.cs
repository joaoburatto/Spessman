using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spessman
{
    public class ConsoleCommandHandler : NetworkBehaviour
    {
        // TODO: Add a dropdown for commands that can have autocomplete
        public static ConsoleCommandHandler singleton { get; private set; }
        
        private void Awake()
        {
            if (singleton != null) Destroy(gameObject);
            singleton = this;
        }
        
        public void ExecuteCommand(string command)
        {
            NetworkManager networkManager = NetworkManager.singleton;
            
            string cmd = GetFirstWord(command);
            string[] args = GetArgs(command);

            Debug.Log("cmd: " + cmd + " args: " + args[0]);
            
            switch (cmd)
            {
                case "quit":
                    Application.Quit();
                    break;
                case "disconnect":
                    if (isServer) networkManager.StopHost();
                    if (isClient) networkManager.StopClient();
                    break;
                case "spawn_item":
                    SpawnItem(args[0]);
                    break;
            }
        }

        [Command(ignoreAuthority = true)]
        private void SpawnItem(string name)
        {
            GameObject item = AssetData.Items.GetAsset(name);

            LocalPlayerManager player = LocalPlayerManager.singleton;
            Quaternion rotation = Quaternion.Euler(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            
            item = Instantiate(item, player.GetSoulEntityPosition(true), rotation);
            
            NetworkServer.Spawn(item);
        }

        private string GetFirstWord(string command)
        {
            string[] commandArray;

            commandArray = command.Split(" "[0]);

            return commandArray[0];
        }

        private string[] GetArgs(string command)
        {
            string[] commandArray;

            commandArray = command.Split(" "[0]);

            List<string> commandList = commandArray.ToList();
            commandList.RemoveAt(0);

            commandArray = commandList.ToArray();
            return commandArray;
        }
    }
}