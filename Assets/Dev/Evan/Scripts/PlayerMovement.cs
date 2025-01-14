using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using System;

public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float airMultiplier;

    [Header("Keybinds")]
    public GameObject pausePanel;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool _isGrounded;

    public bool haveLight, haveJetpack, haveTelecommande, part1, part2, part3;
    private bool flashLightActivated = false;
    public GameObject light;
    public int zone;
    private bool _pauseBool;
    public Transform orientation;

    float _horizontalInput;
    float _verticalInput;
    float _fireInput;
    float _pauseInput;
    Vector3 _moveDirection;
    public LightSphere[] _TargetsSphere;
    public ControlPanel[] _TargetsPanel;
    private Rigidbody _rb;
    Vector3 RespawnTransform; 
    
    public float DetectRange = 10;
    public float DetectAngle = 45;
    private bool isInAngle, isInRange, isNotHidden;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        useObject();
        pauseGame();
        //Ground check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
    
        MyInput();
        SpeedControl();
        if (flashLightActivated)
        {
            FlashlightDetect();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _fireInput = Input.GetAxisRaw("Fire1");
        _pauseInput = Input.GetAxisRaw("Escape");
        
    }

    private void MovePlayer()
    {
        if (_fireInput != 0 || _verticalInput != 0 || _horizontalInput != 0)
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
                    if (haveJetpack && _fireInput != 0)
                    {
                        _moveDirection.Set(_verticalInput, _fireInput, _horizontalInput);
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

    private void pauseGame()
    {
        if (Input.GetButtonDown("Escape"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;

        }
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
                    isInRange = false;
                    isNotHidden = false;
                    for (int i = 0; i < _TargetsPanel.Length; i++)
                    {
                        if (Vector3.Distance(transform.position, _TargetsPanel[i].transform.position) < DetectRange)
                        {
                            isInRange = true;
                        }

                        RaycastHit hit;
                        if (Physics.Raycast(transform.position, (_TargetsPanel[i].transform.position - transform.position), out hit, Mathf.Infinity))
                        {
                            if (hit.transform == _TargetsPanel[i].transform)
                            {
                                isNotHidden = true;
                            }
                        }

                        if (isInRange && isNotHidden)
                        {
                            _TargetsPanel[i].activation();
                        }
                    }
                    break;
                case 2:
                    if (haveLight)
                    {
                        if (flashLightActivated) 
                        { 
                            flashLightActivated = false;
                            light.SetActive(false); 
                        }
                        else 
                        { 
                            flashLightActivated = true;
                            light.SetActive(true); 
                        }
                    }
                    break;
                case 3:
                    
                    break;
                default:
                    break;
            }
        }
    }

    void FlashlightDetect()
    {
        isInAngle = false;
        isInRange = false;
        isNotHidden = false;
        for (int i = 0; i < _TargetsSphere.Length; i++)
        {
            if (Vector3.Distance(transform.position, _TargetsSphere[i].transform.position) < DetectRange)
            {
                isInRange = true;
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, (_TargetsSphere[i].transform.position - transform.position), out hit, Mathf.Infinity))
            {
                if (hit.transform == _TargetsSphere[i].transform)
                {
                    isNotHidden = true;
                }
            }

            Vector3 side1 = _TargetsSphere[i].transform.position - transform.position;
            Vector3 side2 = transform.forward;
            float angle = Vector3.SignedAngle(side1, side2, Vector3.up);
            if (angle < DetectAngle && angle > -1 * DetectAngle)
            {
                isInAngle = true;
            }

            if (isInAngle && isInRange && isNotHidden)
            {
                _TargetsSphere[i].activation();
            }
        }
    }
    public void win()
    {
        if (part1 && part3 && part2)
        {
            
        }
    }
}