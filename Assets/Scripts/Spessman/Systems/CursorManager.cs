using System;
using UnityEngine;

namespace Spessman
{
    public class CursorManager : MonoBehaviour
    {
        public static CursorManager singleton { get; private set; }


        public void SetCursorState(bool state)
        {
            if (state)
            {
                SetCursorUnlocked();
            }
            else
            {
                SetCursorBlocked();
            }
        }

        public void SetCursorBlocked()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SetCursorUnlocked()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        private void Awake()
        {
            if (singleton != null) Destroy(gameObject);
            singleton = this;
        }
    }
}