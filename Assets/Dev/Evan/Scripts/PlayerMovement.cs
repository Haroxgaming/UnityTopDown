using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("HUD")]
    public TMP_Text textPart;
    public TMP_Text fullText;
    public int numberPart = 3;
    public Image imagePart1;
    public Image imagePart2;
    public Image imagePart3;
    public GameObject imageobject1;
    public GameObject imageobject2;
    public GameObject imageobject3;
    public GameObject HUD;
    YieldInstruction wait = new WaitForSeconds(5.0f);
    public AudioSource musiqueSource;
    public AudioSource deathSource;
    public AudioSource FlashlightSourceON;
    public AudioSource FlashlightSourceOFF;
    public AudioSource telecomandeSource;

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
    private bool jetpackReload;
    public float jetpackTimer = 2.0f;
    public bool haveLight, haveJetpack, haveTelecommande;
    
    public bool part1 = false;
    private bool part1Make = false;
    public bool part2 = false;
    private bool part2Make = false;
    public bool part3 = false;
    private bool part3Make = false;
    private bool flashLightActivated = false;
    public new GameObject light;
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
    
    public float DetectRange = 4;
    public float DetectAngle = 20;
    private bool isInAngle, isInRange, isNotHidden;

    private int _animIDrun;
    private int _animIDGrounded;
    private Animator _animator;
    float rotationSpeed = 5.0f;
    private bool _hasAnimator;
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _hasAnimator = TryGetComponent(out _animator);
    }

    private void Update()
    {
        _hasAnimator = TryGetComponent(out _animator);
        useObject();
        pauseGame();
        //Ground check
        _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        zoneChange();
        MyInput();
        SpeedControl();
        if (flashLightActivated)
        {
            FlashlightDetect();
        }
		if (!jetpackReload)
		{
			if (jetpackTimer >= 2.0f)
        	{
				jetpackTimer = 2.0f;
        	    jetpackReload = true;
        	}
			else
			{
				jetpackTimer += Time.deltaTime;
			}
		}
        if (part1 && !part1Make)
        {
            numberPart--;
            part1Make = true;
            textPart.text = numberPart.ToString();
            imagePart1.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (part2 && !part2Make)
        {
            numberPart--;
            part2Make = true;
            textPart.text = numberPart.ToString();
            imagePart2.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        if (part3 && !part3Make)
        {
            numberPart--;
            part3Make = true;
            textPart.text = numberPart.ToString();
            imagePart3.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }

        if (numberPart == 0)
        {
            textPart.text = " ";
            fullText.text = "- Goodbye";
        }

        switch (zone)
        {
            case 0:
                imageobject1.SetActive(false);
                imageobject2.SetActive(false);
                imageobject3.SetActive(false);
                break;
            case 1:
                if (haveTelecommande)
                {
                    imageobject1.SetActive(false);
                    imageobject2.SetActive(false);
                    imageobject3.SetActive(true);
                }
                else
                {
                    imageobject1.SetActive(false);
                    imageobject2.SetActive(false);
                    imageobject3.SetActive(false);
                }
                break;
            case 2:
                if (haveLight)
                {
                    imageobject1.SetActive(false);
                    imageobject2.SetActive(true);
                    imageobject3.SetActive(false);
                }
                else
                {
                    imageobject1.SetActive(false);
                    imageobject2.SetActive(false);
                    imageobject3.SetActive(false);
                }
                break;
            case 3:
                if (haveJetpack)
                {
                    imageobject1.SetActive(true);
                    imageobject2.SetActive(false);
                    imageobject3.SetActive(false);
                }
                else
                {
                    imageobject1.SetActive(false);
                    imageobject2.SetActive(false);
                    imageobject3.SetActive(false);
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        GroundedCheck();
    }
    
    private void GroundedCheck()
    {
        if (transform.position.y > 1.75f)
        {
            _animator.SetBool("Grounded", false);
        }
        else if (transform.position.y < 1f)
        {
            _animator.SetBool("Grounded", false);
        }
        else
        {
            _animator.SetBool("Grounded", true);
        }
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        
    }
    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        _fireInput = Input.GetAxisRaw("Fire1");
        _pauseInput = Input.GetAxisRaw("Escape");
        
    }

    void zoneChange()
    {
        if (zone != 2)
        {
            flashLightActivated = false;
            light.SetActive(false); 
        }
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
                    if (_verticalInput != 0 || _horizontalInput != 0)
                    {
                        _animator.SetBool("run", true);
                    }
                    break;
                case 1:
                    _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                    _rb.linearVelocity = _moveDirection * moveSpeed;
                    if (_verticalInput != 0 || _horizontalInput != 0)
                    {
                        _animator.SetBool("run", true);
                    }                    
                    break;
                case 2: 
                    _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                    _rb.linearVelocity = _moveDirection * moveSpeed;
                    if (_verticalInput != 0 || _horizontalInput != 0)
                    {
                        _animator.SetBool("run", true);
                    }                    
                    break;
                case 3:
                    if (haveJetpack && _fireInput != 0 && jetpackReload)
                    {
                        musiqueSource.Play();
                        _moveDirection.Set(_verticalInput, _fireInput, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                        var pos = transform.position;
                        pos.y =  Mathf.Clamp(transform.position.y, -20.0f, 4.0f);
                        transform.position = pos;
                        jetpackTimer -= Time.deltaTime;
                        _animator.SetBool("Grounded", false);
                        if (jetpackTimer <= 0)
                        {
                            jetpackReload = false;
                            musiqueSource.Stop();
                        }
                    }
                    else if (transform.position.y < 1.7f)
                    {
                        _moveDirection.Set(_verticalInput, 0, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
						jetpackReload = false;
                        _animator.SetBool("run", true);
                        musiqueSource.Stop();
                    }
                    else if (transform.position.y < 1.55f)
                    {
                        _moveDirection.Set(_verticalInput, -1, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                        _animator.SetBool("Grounded", false);
                        musiqueSource.Stop();
                    }
                    else
                    {
                        _moveDirection.Set(_verticalInput, -1, _horizontalInput);
                        _rb.linearVelocity = _moveDirection * moveSpeed;
                        _animator.SetBool("Grounded", false);
                        musiqueSource.Stop();
                    }
                    break;
            }
            if (_moveDirection.sqrMagnitude > 0.01f)
            {
                Vector3 direction = new Vector3(_moveDirection.x, 0, _moveDirection.z).normalized;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            _animator.SetBool("run", false);
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
        deathSource.Play();
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
            HUD.SetActive(false);
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
                    if (haveTelecommande)
                    {
                        for (int i = 0; i < _TargetsPanel.Length; i++)
                        {
                            isInRange = false;
                            isNotHidden = false;
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
                                telecomandeSource.Play();
                                _TargetsPanel[i].activation();
                            }
                        }
                    }
                    break;
                case 2:
                    if (haveLight)
                    {
                        if (flashLightActivated) 
                        { 
                            FlashlightSourceOFF.Play();
                            flashLightActivated = false;
                            light.SetActive(false); 
                        }
                        else 
                        { 
                            FlashlightSourceON.Play();
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
        for (int i = 0; i < _TargetsSphere.Length; i++)
        {
            isInAngle = false;
            isInRange = false;
            isNotHidden = false;
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
            SceneManager.LoadScene(2);
        }
    }
}