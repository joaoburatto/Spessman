using Mirror;
using UnityEngine;

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
        
        public void Command(string command)
        {
            NetworkManager networkManager = NetworkManager.singleton;
            switch (command)
            {
                case "quit":
                    Application.Quit();
                    break;
                case "disconnect":
                    if (isServer) networkManager.StopHost();
                    if (isClient) networkManager.StopClient();
                    break;
            }
        }
    }
}