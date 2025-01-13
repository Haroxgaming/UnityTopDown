using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;

    bool activation;

    private Vector3 targetPos, newPos;

    public Vector3 minPos, maxPos;
    public void open()
    {
        activation = true;
    }
    
    void Update()
    {
        if (transform.position != player.position && activation)
        {
            targetPos = player.position;

            Vector3 camBoundaryPos = new Vector3(
                Mathf.Clamp(targetPos.x, minPos.x, maxPos.x),
                Mathf.Clamp(targetPos.y, minPos.y, maxPos.y),
                Mathf.Clamp(targetPos.z, minPos.z, maxPos.z));

            newPos = Vector3.Lerp(transform.position, camBoundaryPos, smoothSpeed);
            transform.position = newPos;
        }
    }
}
