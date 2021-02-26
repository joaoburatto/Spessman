using System;
using System.Net;
using Mirror;
using TMPro;
using UnityEngine;
using NetworkManager = Spessman.Networking.NetworkManager;

namespace Spessman.UIHelper
{
    public class MainMenuUIHelper : MonoBehaviour
    {
        public TMP_InputField ipAddressInputField;

        private Uri TryParseIpAddress()
        {
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "tcp4";
            if (ipAddressInputField &&
                IPAddress.TryParse(ipAddressInputField.text, out IPAddress address))
            {
                uriBuilder.Host = address.ToString();
            }
            else
            {
                uriBuilder.Host = "localhost";
            }

            var uri = new Uri(uriBuilder.ToString(), UriKind.Absolute);
            return uri;
        }

        public void OnJoinButtonPressed()
        {
            var uriAdress = TryParseIpAddress();
            NetworkManager networkManager = NetworkManager.singleton;
            networkManager.StartClient(uriAdress);
        }
        
        

        public void OnHostButtonPressed()
        {
            NetworkManager networkManager = NetworkManager.singleton;
            networkManager.StartHost();
        }
    }
}