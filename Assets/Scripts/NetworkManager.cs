using System;
using TMPro;

namespace Spessman.Networking
{
    public class NetworkManager : Mirror.NetworkManager
    {
        // Handles button-inputfield interaction to connect
        public void StarClientButton(TMP_InputField inputField )
        {
            string ip = inputField.text;

            Uri uri = new Uri(ip);
            
            StartClient(uri);
        }
    }
    
    
}