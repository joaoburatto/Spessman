using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace Spessman
{
    public class GeneralSettingsManager : MonoBehaviour
    {
        public static GeneralSettingsManager singleton { get; private set; }

        public float mouseSensitivityX;
        public float mouseSensitivityY;

        public Slider[] mouseSensitivitySliders;
        
        public event System.Action<float> mouseSensitivityXChanged;
        public event System.Action<float> mouseSensitivityYChanged;

        public int graphicsQuality;
        public Button[] graphicsButtons;

        public PostProcessProfile[] postProcessing;
        public PostProcessVolume postProcessVolume;
        
        private void Awake()
        {
            if (singleton != null) Destroy(gameObject);
            singleton = this;
        }

        private void Start()
        {
            graphicsButtons[graphicsQuality].interactable = false;
            SetGraphicsQuality(graphicsQuality);

            postProcessVolume.profile = postProcessing[graphicsQuality];
            
            mouseSensitivitySliders[0].value = mouseSensitivityX;
            mouseSensitivitySliders[1].value = mouseSensitivityY;
        }

        public void SetSensitivityX(Slider slider)
        {
            float value = slider.value;
            mouseSensitivityX = value;
            mouseSensitivityXChanged?.Invoke(value);
        }

        public void SensitivityY(Slider slider)
        {
            float value = slider.value;
            mouseSensitivityY = value;
            mouseSensitivityYChanged?.Invoke(value);
        }

        public void SetGraphicsQuality(int quality)
        {
            graphicsQuality = quality;
            
            QualitySettings.SetQualityLevel(quality);
            postProcessVolume.profile = postProcessing[graphicsQuality];

            graphicsButtons[quality].interactable = false;
            

            for (int i = 0; i < graphicsButtons.Length; i++)
            {
                if (i != quality)
                {
                    graphicsButtons[i].interactable = true;
                }
            }
        }
        
    }
}