using UnityEngine;

public class KillZone : MonoBehaviour
{
    public PlayerMovement player;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Respawn();
        }
    }
}
