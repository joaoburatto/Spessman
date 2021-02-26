using System;
using System.Net;
using Spessman.Networking;
using TMPro;
using UnityEngine;

namespace Spessman.UIHelper
{
    public class MainMenuUIHelper : MonoBehaviour
    {
        public TMP_InputField ipAddressInputField;
        public TMP_InputField nicknameInputField;
        
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
            string name = nicknameInputField.text;

            if (name == "")
            {
                name = "Guest";
            }
            networkManager.name = name;
            networkManager.StartClient(uriAdress);
        }
    }
}