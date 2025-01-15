using UnityEngine;

public class WinCheck : MonoBehaviour
{
    public PlayerMovement playerRef;
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerRef.win();
        }
    }
}
