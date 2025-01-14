using UnityEngine;
using System;

public class Door : MonoBehaviour
{
    public float smoothSpeed;
    private Vector3 targetPos, newPos;
    private Vector3 temp;
    public bool activated;
    public Vector3 minPos, maxPos;
    public void open()
    {
        if (activated)
        {
            minPos = temp;
            activated = false;
        }
        else
        {
            temp = minPos;
            minPos = maxPos;
            activated = true;
        }
    }

    void Update()
    {
        Vector3 camBoundaryPos = new Vector3(
            Mathf.Clamp(targetPos.x, minPos.x, maxPos.x),
            Mathf.Clamp(targetPos.y, minPos.y, maxPos.y),
            Mathf.Clamp(targetPos.z, minPos.z, maxPos.z));

        newPos = Vector3.Lerp(transform.position, camBoundaryPos, smoothSpeed);
        transform.position = newPos;
    }
}
