using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public CharacterController controller;
 
    public Transform orientation;
    
    public float moveSpeed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;
 
    Vector3 velocity;
    
 
    // Update is called once per frame
    void Update()
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
 
        velocity.y += gravity * Time.deltaTime;
 
        controller.Move(velocity * Time.deltaTime);
    }  
}
