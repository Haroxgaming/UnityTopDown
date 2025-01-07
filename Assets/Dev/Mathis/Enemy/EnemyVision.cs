using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public float DetectRange = 10;
    private bool isInAngle, isInRange, isNotHidden;

    public GameObject Player;
    TMP_Text RangeText
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isInAngle = false;
        isInRange = false;
        isNotHidden = false;

        if (Vector3.distance(transform.position, Player.transform.position) < DetectRange)
        {
            isInRange = true;
            RangeText.text = "In Range";
            RangeText.color = Color.red;
        }
        else
        {
            RangeText.text = "Not In Range";
            RangeText.color = Color.green;
        }
    }
}
