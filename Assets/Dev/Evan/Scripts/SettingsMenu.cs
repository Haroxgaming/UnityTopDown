using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMix;
    
    public void SetVolume(float volume)
    {
        audioMix.SetFloat("Volume", volume);
    }

    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }
}
