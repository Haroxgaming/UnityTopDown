using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioSource source;
    public int type;
    public PlayerMovement player;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            switch (type)
            {
                case 0:
                    source.Play();
                    player.haveTelecommande = true;
                    break;
                case 1:
                    source.Play();
                    player.haveJetpack = true;
                    break;
                case 2:
                    source.Play();
                    player.haveLight = true;
                    break;
                case 3:
                    source.Play();
                    player.part1 = true;
                    break;
                case 4:
                    source.Play();
                    player.part2 = true;
                    break;
                case 5:
                    source.Play();
                    player.part3 = true;
                    break;
            }
            Destroy(gameObject);
        }
    }
}
