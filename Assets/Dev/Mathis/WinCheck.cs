using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public PlayerMovement playerRef;
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerRef.win();
        }
    }
}
