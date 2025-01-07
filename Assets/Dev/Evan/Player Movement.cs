using System;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public CharacterController controller;
 
    public Transform orientation;
    
    float horizontalInput;
    float verticalInput;
    
    public float moveSpeed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    Vector3 MoveDirection;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = false;
    }

    private void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        MoveDirection = new Vector3(horizontalInput, 0, verticalInput);
        rb.AddForce(MoveDirection.normalized * moveSpeed * , ForceMode.Impulse);
    }
    // Update is called once per frame
   /* void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Debug.Log(x + "  -  " + z);
 
        //right is the red Axis, foward is the blue axis
        Vector3 move = transform.right * x + transform.forward * z;
 
        controller.Move(move * (moveSpeed * Time.deltaTime));
 
        //check if the player is on the ground so he can jump
        if (Input.GetButtonDown("Jump"))
        {
            //the equation for jumping
            MoveDirection.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        MoveDirection.y += gravity * Time.deltaTime;
 
        controller.Move(MoveDirection * Time.deltaTime);
    }  */
}
