using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int type;
    public PlayerMovement player;
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            /*switch (type)
            {
                case 0:
                    
            }*/
            Destroy(gameObject);
        }
    }
}
