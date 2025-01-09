using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Dev.Evan.Scripts
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMix; 
        public TMP_Dropdown resolutionDropdown;
        
        Resolution[] _resolutions;

        void Start()
        {
            _resolutions = Screen.resolutions;
            resolutionDropdown.ClearOptions();
            List<string> options = new List<string>();

            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);
            }
            resolutionDropdown.AddOptions(options);
        }
    
        public void SetVolume(float volume)
        {
            audioMix.SetFloat("Volume", volume);
        }

        public void SetQuality (int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
        }
        
    }
}
