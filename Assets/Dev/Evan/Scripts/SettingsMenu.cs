using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;

using Slider = UnityEngine.UI.Slider;


namespace Dev.Evan.Scripts
{
    public class SettingsMenu : MonoBehaviour
    {
        public AudioMixer audioMix; 
        public float volumeValue;
        public Slider volumeSlider;
        public TMP_Dropdown resolutionDropdown;
        public TMP_Dropdown qualityDropdown;
        
        
        Resolution[] _resolutions;

        void Start()
        {
            qualityDropdown.value = PlayerPrefs.GetInt("Quality");
            volumeSlider.value = PlayerPrefs.GetFloat("Volume");
            
            _resolutions = Screen.resolutions; // Récupère les résolutions disponibles
            resolutionDropdown.ClearOptions(); // Efface les options du Dropdown
            List<string> options = new List<string>();

            int currentResolutionIndex = -1;

            // Ajoute les résolutions au menu déroulant
            for (int i = 0; i < _resolutions.Length; i++)
            {
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                // Compare la résolution actuelle
                if (_resolutions[i].width == Screen.width &&
                    _resolutions[i].height == Screen.height &&
                    _resolutions[i].refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
                {
                    currentResolutionIndex = i;
                }
            }

            // Ajoute les options au Dropdown
            resolutionDropdown.AddOptions(options);

            // Définit la résolution actuelle
            if (currentResolutionIndex != -1)
            {
                resolutionDropdown.value = currentResolutionIndex; // Sélectionne l'option correcte
            }
            else
            {
                resolutionDropdown.value = 0; // Par défaut, sélectionne la première option
            }
            
            // Actualise l'affichage du menu déroulant
            resolutionDropdown.RefreshShownValue();
        }

        void Update()
        {
            audioMix.SetFloat("Volume", volumeValue);
            PlayerPrefs.SetFloat("Volume", volumeValue);
            PlayerPrefs.SetInt("Quality", qualityDropdown.value);
            qualityDropdown.name = qualityDropdown.options[qualityDropdown.value].text;
        }
        
        public void SetVolume(float volume)
        {
            audioMix.SetFloat("Volume", volumeValue);
            volumeValue = volume;
        }

        public void SetQuality (int qualityIndex)
        {
            int pickedEntryIndex = qualityDropdown.value;
            string selectedOption = qualityDropdown.options[pickedEntryIndex].text;
            Debug.Log(selectedOption);
            QualitySettings.SetQualityLevel(pickedEntryIndex);
            qualityDropdown.RefreshShownValue();
        }

        public void SetResolution(int resolutionIndex)
        {
            Resolution resolution = _resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
