using UnityEngine;

namespace Spessman
{
    public class Entity : MonoBehaviour
    {
        private string name;
        private Transform nameDisplay;

        public void SetName(string name)
        {
            this.name = name;
        }
    }
}