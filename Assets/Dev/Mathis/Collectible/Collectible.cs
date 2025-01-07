using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (gameObject.tag)
        {
            case FlashLight:
                
        }
    }
}
