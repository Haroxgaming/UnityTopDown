using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _isGrounded;

    public bool haveLight;
    private bool flashLightActivated = false;
    public GameObject light;
    public bool haveJetpack;
    public int zone;
    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;
    float _FireInput;
    Vector3 _moveDirection;

    private Rigidbody _rb;
    Vector3 RespawnTransform; 

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        useObject();
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
        _FireInput = Input.GetAxisRaw("Fire1");
    }

    private void MovePlayer()
    {
        if (_FireInput != 0 || _verticalInput != 0 || _horizontalInput != 0)
        {
            switch (zone)
            {
                case 0:
                    _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                    _rb.linearVelocity = _moveDirection * moveSpeed;
                    break;
                case 1:
                    _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                    _rb.linearVelocity = _moveDirection * moveSpeed;
                    break;
                case 2: 
                    _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                    _rb.linearVelocity = _moveDirection * moveSpeed;
                    break;
                case 3:
                    if (haveJetpack && _FireInput != 0)
                    {
                        _moveDirection.Set(_verticalInput, _FireInput, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                        var pos = transform.position;
                        pos.y =  Mathf.Clamp(transform.position.y, -20.0f, 4.0f);
                        transform.position = pos;
                    }
                    else if (transform.position.y < 1.7f)
                    {
                        _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                    }
                    else
                    {
                        _moveDirection.Set(_verticalInput, -1, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                    }
                    break;
            }
        }
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
    
    public void Respawn()
    {
        transform.position = RespawnTransform;
    }

	public void setRespawnTransform()
    {
        RespawnTransform = transform.position;
    }

    private void useObject()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            switch (zone)
            {
                case 0:
                    break;
                case 1:
                    //infiltration
                    break;
                case 2:
                    if (haveLight)
                    {
                        if (!flashLightActivated) 
                        { 
                            flashLightActivated = true;
                            light.SetActive(false); 
                        }
                        else 
                        { 
                            flashLightActivated = false;
                            light.SetActive(true); 
                        }
                    }
                    //FlashLight
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }
    }
}