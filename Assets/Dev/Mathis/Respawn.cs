using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public PlayerMovement player;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.RespawnTransform = transform.position;
        }
    }
}