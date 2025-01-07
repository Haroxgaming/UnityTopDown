using UnityEngine;

public class LightSphere : MonoBehaviour
{
    public GameObject lightOn, lightOff, switchOn, switchOff;
    public bool toggle;
    public AudioSource switchSound;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (toggle == true)
                {
                    lightOn.SetActive(true);
                    lightOff.SetActive(false);
                    switchOn.SetActive(true);
                    switchOff.SetActive(false);
                }
                else
                {
                    lightOn.SetActive(false);
                    lightOff.SetActive(true);
                    switchOn.SetActive(false);
                    switchOff.SetActive(true);
                }
            }
        }
    }
}
