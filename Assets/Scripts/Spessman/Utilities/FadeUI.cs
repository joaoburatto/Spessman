using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Spessman
{
    public class FadeUI : MonoBehaviour
    {
        public Animator animator;

        public void Fade(bool state)
        {
            if (!animator.enabled) animator.enabled = true;
            if (!animator.gameObject.activeSelf) animator.gameObject.SetActive(true);

            animator.SetBool("Fade", state);
        }
    }
}
