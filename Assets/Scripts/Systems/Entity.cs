using TMPro;
using UnityEngine;

namespace Spessman
{
    public class Entity : MonoBehaviour
    {
        private string entityName;

        public void SetName(string name)
        {
            transform.name = name;
            entityName = name;
        }
    }
}