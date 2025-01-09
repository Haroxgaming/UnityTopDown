using UnityEngine;

namespace Dev.Evan.Scripts
{
    public class PlayerMouvement : MonoBehaviour
    {
 
        [Header("Movement")]
        public float moveSpeed;
        public float groundDrag;
        public float jumpForce;
        public float jumpCd;
        public float airMultiplier;
        bool _readyToJump = true;
    
        [Header("Keybinds")]
        public KeyCode jumpKey = KeyCode.Space;
    
        [Header("Ground Check")]
        public float playerHeight;
        public LayerMask whatIsGround;
        bool _isGrounded;
    
        public Transform orientation;
    
        float _horizontalInput;
        float _verticalInput;
 
        Vector3 _moveDirection;

        private Rigidbody _rb;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
        }

        private void Update()
        {
            //Ground check
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        
            MyInput();
            SpeedControl();
            //Handle drag
            if(_isGrounded)
                _rb.linearDamping = groundDrag;
            else
                _rb.linearDamping = 0;
        
        }

        private void FixedUpdate()
        {
            MovePlayer();
        }
    
        private void MyInput()
        {
            _horizontalInput = Input.GetAxisRaw("Horizontal");
            _verticalInput = Input.GetAxisRaw("Vertical");
        
            //When to jump
            if (Input.GetKeyDown(jumpKey) && _readyToJump && _isGrounded)
            {
                _readyToJump = false;
                Jump();
                Invoke(nameof(ResetJump), jumpCd);
            }
        }

        private void MovePlayer()
        {
            //Calculate movement direction
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        
            //On ground
            if (_isGrounded)
                _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force);
        
            //In air
            else if (!_isGrounded)
                _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force);
        }

        private void SpeedControl()
        {
            Vector3 flatVel = new Vector3(_rb.linearVelocity.x, 0f , _rb.linearVelocity.z);
        
            //limit velocity if needed
            if (flatVel.magnitude > moveSpeed)
            {
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                _rb.linearVelocity = new Vector3(limitedVel.x, _rb.linearVelocity.y, limitedVel.z);
            }
        }

        private void Jump()
        {
            //Reset y velocity
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, 0f, _rb.linearVelocity.z);
        
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        private void ResetJump()
        {
            _readyToJump = true;
        }
    }
}
