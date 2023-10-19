using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HunterScript : MonoBehaviour
{
    NavMeshAgent hunter;

    [Header ("Layer Mask")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask obstacleMask;

    [SerializeField] Animator hunterAnim;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;
    Vector3 point;

    [Header ("Transform")]
    [SerializeField] Transform player;
    [SerializeField] Transform centrePoint;

    [Header ("Float")]
    [SerializeField] float range;
    [SerializeField] float startWaitTime = 4;
    [SerializeField] float timeToRotate = 2;
    [SerializeField] float huntSpeed = 5;
    [SerializeField] float chaseSpeed = 15;
    [SerializeField] float viewRadius = 15;
    [SerializeField] float viewAngle = 90;
    [SerializeField] float meshResolution = 1f;
    [SerializeField] float edgeDistance = 0.5f;
    [SerializeField] float m_WaitTime;
    [SerializeField] float m_TimeToRotate;

    [Header ("Int")]
    [SerializeField] int edgeIterations = 4;

    [Header("Bool")]
    [SerializeField] bool m_PlayerInRange;
    [SerializeField] bool m_PlayerNear;
    [SerializeField] bool m_IsPatrol;
    [SerializeField] bool m_CaughtPlayer;

    private void Awake()
    {
        hunter = GetComponent<NavMeshAgent>();

        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        hunter.isStopped = false;
        hunter.speed = huntSpeed;
        hunter.SetDestination(point);
    }

    private void Update()
    {
        EnviromentView();

        if (!m_IsPatrol)
        {
            Chasing();
            Debug.Log("Chasing");
        }
        else
        {
            Debug.Log("Huntingss");
            //Hunting();
        }
    }

    private void Chasing()
    {
        m_PlayerInRange = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer)
        {
            Move(chaseSpeed);
            hunter.SetDestination(m_PlayerPosition);
        }
        if(hunter.remainingDistance <= hunter.stoppingDistance)
        {
            if(m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(chaseSpeed);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                hunter.SetDestination(point);
            }
            else
            {
                if(Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    private void Hunting()
    {
        if(m_PlayerNear)
        {
            if(m_TimeToRotate <= 0)
            {
                Move(huntSpeed);
                HuntingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            HuntingPoint(transform.position, range, out point);
            hunter.SetDestination(point);

            if (hunter.remainingDistance <= hunter.stoppingDistance)
            {
                if(m_WaitTime <= 0)
                {
                    Move(huntSpeed);
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

    private void Move(float speed)
    {
        hunter.isStopped = false;
        hunter.speed = speed;
    }

    private void Stop()
    {
        hunter.isStopped = true;
        hunter.speed = 0;
    }

    private void CaughtPlayer()
    {
        m_CaughtPlayer = true;
    }

    private void HuntingPlayer(Vector3 player)
    {
        hunter.SetDestination(player);
        if(Vector3.Distance(centrePoint.position, player) <= 0.3)
        {
            if(m_WaitTime <= 0)
            {
                m_PlayerInRange = false;
                Move(huntSpeed);
                hunter.SetDestination(point);

                if (hunter.remainingDistance <= hunter.stoppingDistance)
                {

                    if (HuntingPoint(centrePoint.position, range, out point))
                    {
                        Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                        hunter.SetDestination(point);
                    }
                }

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

    private bool HuntingPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    void EnviromentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);

        for(int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                }
            }
            if(Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
            if (m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }

}
