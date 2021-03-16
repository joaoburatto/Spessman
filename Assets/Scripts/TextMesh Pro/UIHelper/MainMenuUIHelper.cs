using Mirror;
using System;
using System.Collections;
using System.Net;
using kcp2k;
using Telepathy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spessman.UIHelper
{
    public class MainMenuUIHelper : MonoBehaviour
    {
        public TMP_InputField ipAddressInputField;
        
        public Button joinButton;
        public TMP_Text joinButtonText;

        public bool connecting;

        public Animator animator;

        private void Start()
        {
            KcpConnection.OnConnectionFailed += OnClientFailConnection;
        }

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
            connecting = true;
            animator.SetBool("Toggle", false);
            
            StartCoroutine(ChangeJoinText());
            
        }
        
        public void OnHostButtonPressed()
        {
            NetworkManager networkManager = NetworkManager.singleton;
            networkManager.StartHost();
        }
        
        public void OnClientFailConnection()
        {
            UnityMainThread.wkr.AddJob(delegate
            {
                connecting = false;
                if (!animator.enabled) animator.enabled = true;
                animator.SetBool("Toggle", true);
            });
            
        }

        public IEnumerator ChangeJoinText()
        {
            joinButton.interactable = false;
            while (connecting)
            {
                joinButtonText.text = ".";
                yield return new WaitForSeconds(.2f);
                joinButtonText.text = "..";
                yield return new WaitForSeconds(.2f);
                joinButtonText.text = "...";
                yield return new WaitForSeconds(.2f);
            }
            joinButton.interactable = true;
            joinButtonText.alignment = TextAlignmentOptions.Midline;
            joinButtonText.text = "join";
        }
    }
}