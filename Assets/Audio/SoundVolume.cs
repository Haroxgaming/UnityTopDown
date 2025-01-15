using UnityEngine;

public class SoundVolume : MonoBehaviour
{
    public AudioSource musiqueSource;
    public void Update()
    {
        musiqueSource.volume = AudioListener.volume;
    }
}
