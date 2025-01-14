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
            switch (type)
            {
                case 0:
                    player.haveTelecommande = true;
                    break;
                case 1:
                    player.haveJetpack = true;
                    break;
                case 2:
                    player.haveLight = true;
                    break;
                case 3:
                    player.part1 = true;
                    break;
                case 4:
                    player.part2 = true;
                    break;
                case 5:
                    player.part3 = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
