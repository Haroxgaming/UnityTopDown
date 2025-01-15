using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{
    public Vector3 newCamPos, newPlayerPos;

    CamController camControl;
    public PlayerMovement playerRef;

    public int zoneChange;
    
    void Start()
    {
        camControl = Camera.main.GetComponent<CamController>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            camControl.minPos += newCamPos;
            camControl.maxPos += newCamPos;

            other.transform.position += newPlayerPos;
            playerRef.zone = zoneChange;
        }
    }
}
