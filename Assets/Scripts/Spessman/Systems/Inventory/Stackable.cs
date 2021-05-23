using System;
using UnityEngine;

namespace Spessman.Inventory
{
    [RequireComponent(typeof(Item))]
    public class Stackable : MonoBehaviour
    {
        public int maxStack;
        public int amountInStack;

        private void OnValidate()
        {
            if (maxStack < 2)
            {
                maxStack = 2;
            }

            if (amountInStack < 1)
            {
                amountInStack = 1;
            }
            else if (amountInStack > maxStack)
            {
                amountInStack = maxStack;
            }
            
        }

        public string GetDescription(GameObject _)
        {
            return $"{amountInStack} in stack";
        }
		
        public virtual string GetName(GameObject examinator)
        {
            return gameObject.name;
        }		
    }
}
