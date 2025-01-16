using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class AIControler : MonoBehaviour
{
    public AudioSource musiqueSource;
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 2;
    public float timeToRotate = 2;
    public float speedWalk = 6;
    public float speedRun = 9;

    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime;
    float m_TimeToRotate;
    bool m_PlayerNear;
    bool m_IsPatrol;
    bool m_CaughtPlayer;
    
    public float DetectRange = 10;
    public float DetectAngle = 45;
    private bool isInAngle, isInRange, isNotHidden, isDetected;
    private Quaternion initialArmRRot, initialArmLRot, initialLegRRot, initialLegLRot;
    public float limbSwingAmplitude = 60f;


    public GameObject Player;
    
    public PlayerMovement player;

    public GameObject head, body, armR, armL, legR, legL;

    void Start()
    {
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        if (armR != null) initialArmRRot = armR.transform.localRotation;
        if (armL != null) initialArmLRot = armL.transform.localRotation;
        if (legR != null) initialLegRRot = legR.transform.localRotation;
        if (legL != null) initialLegLRot = legL.transform.localRotation;
    }
    
    void Update()
    {
        DetectPlayer();
        AnimateLimbs(15);
        if (!m_IsPatrol)
        {
            Chasing();
        }
        else
        {
            Patroling();
        }
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(speedRun);
            navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position)>= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }
    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    
    void AnimateLimbs(float move)
    {
        if (move == 0) return; 

        float limbSwing = Mathf.Sin(Time.time * 5f) * limbSwingAmplitude;

        if (armR != null)
        {
            Quaternion targetRotation = initialArmRRot * Quaternion.Euler(limbSwing, 0, 0);
            armR.transform.localRotation = Quaternion.Lerp(armR.transform.localRotation, targetRotation, Time.deltaTime);
        }

        if (armL != null)
        {
            Quaternion targetRotation = initialArmLRot * Quaternion.Euler(-limbSwing, 0, 0);
            armL.transform.localRotation = Quaternion.Lerp(armL.transform.localRotation, targetRotation, Time.deltaTime);
        }

        if (legR != null)
        {
            Quaternion targetRotation = initialLegRRot * Quaternion.Euler(limbSwing, 0, 0);
            legR.transform.localRotation = Quaternion.Lerp(legR.transform.localRotation, targetRotation, Time.deltaTime);
        }
        
        if (legL != null)
        {
            Quaternion targetRotation = initialLegLRot * Quaternion.Euler(-limbSwing, 0, 0);
            legL.transform.localRotation = Quaternion.Lerp(legL.transform.localRotation, targetRotation, Time.deltaTime);
        }
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void DetectPlayer()
    {
        isInAngle = false;
        isInRange = false;
        isNotHidden = false;

        if (Vector3.Distance(transform.position, Player.transform.position) < DetectRange)
        {
            isInRange = true;
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, Mathf.Infinity))
        {
            if (hit.transform == Player.transform)
            {
                isNotHidden = true;
            }
        }

        Vector3 side1 = Player.transform.position - transform.position;
        Vector3 side2 = transform.forward;
        float angle = Vector3.SignedAngle(side1, side2, Vector3.up);
        if (angle < DetectAngle && angle > -1 * DetectAngle)
        {
            isInAngle = true;
        }

        if (isInAngle && isInRange && isNotHidden)
        {
            m_PlayerPosition = Player.transform.position;
            m_IsPatrol = false;
            isDetected = true;

        }
        else if (isDetected && Vector3.Distance(transform.position, Player.transform.position) < (DetectRange -4))
        {
            m_PlayerPosition = Player.transform.position;
            m_IsPatrol = false;
            isDetected = true;
        }
        else
        {
            m_IsPatrol = true;
            isDetected = false;
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            musiqueSource.Play();
            player.Respawn();
        }
    }
}