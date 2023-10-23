using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MR_HunterScript : MonoBehaviour
{
    NavMeshAgent _Hunter;

    [Header("Layer Mask")]
    [SerializeField] LayerMask playerMask;

    [Header("Transform")]
    [SerializeField] Transform _Player;
    [SerializeField] Transform centerPoint;

    [Header("Float")]
    private float range;
    [SerializeField] float patrolSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float viewRadius;
    [SerializeField] float viewAngle = 90;

    [Header("Bool")]
    [SerializeField] bool playerInRange;
    [SerializeField] bool playerNear;
    [SerializeField] bool onPatrol;
    [SerializeField] bool playerCloseUp;

    [Header("Timers")]
    [SerializeField] float spawnTime;
    [SerializeField] float maxSpawnTime;
    [SerializeField] float huntTime;
    [SerializeField] float maxHuntTime;
    [SerializeField] float finalSecs;

    [Header("Animation")]
    [SerializeField] Animator huntAnim;


    Vector3 playerLastPosition = Vector3.zero;
    Vector3 playerPosition = Vector3.zero;
    Vector3 rayPoint;


    private void Awake()
    {
        _Hunter = GetComponent<NavMeshAgent>();
        _Hunter.isStopped = false;
        _Hunter.speed = patrolSpeed;
        _Hunter.SetDestination(centerPoint.position);

        playerPosition = Vector3.zero;

        onPatrol = true;
        playerCloseUp = false;
        playerInRange = false;

        spawnTime = maxSpawnTime;
        huntTime = maxHuntTime;
    }

    private void Update()
    {
        if (spawnTime > 0)
        {
            Stop();
            spawnTime -= Time.deltaTime;
            huntTime = maxHuntTime;
        }

        if (huntTime <= 0)
        {
            huntTime = 0;
            Death();
        }
        if (playerCloseUp == true)
        {
            Attack();
            return;
        }
        if (playerInRange == false && playerCloseUp == false)
        {
            Patrol();
        }
        else if(playerCloseUp == false)
        {
            Chase();
        }
    }

    private void Death()
    {
        finalSecs -= Time.deltaTime;
        Stop();
        _Hunter.speed = 0;
        huntAnim.SetBool("Despawn", true);
        huntAnim.SetBool("Patrol", false);
        huntAnim.SetBool("Chasing", false);
        huntAnim.SetBool("CanAttack", false);

        if (finalSecs <= 0)
        {
            Destroy(gameObject);
        }

        return;
    }

    public bool PatrolOrChase(bool choice)
    {
        playerInRange = choice;
        return playerInRange;
    }

    public bool CanAttack(bool choice)
    {
        playerCloseUp = choice;
        return playerCloseUp;
    }

    private void Attack()
    {
        Stop();
        _Hunter.speed = 0;
        huntAnim.SetBool("CanAttack", true);
        return;
    }

    private void Chase()
    {
        if (spawnTime > 0)
        {
            return;
        }
        if(huntTime > 0)
        {
            huntTime = maxHuntTime;
        }

        huntAnim.SetBool("CanAttack", false);
        huntAnim.SetBool("Patrol", false);
        huntAnim.SetBool("Chasing", true);

        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        Move(chaseSpeed);
        _Hunter.SetDestination(playerPosition);

    }

    private void Patrol()
    {
        while(huntTime > 0)
        {
            huntTime -= Time.deltaTime;
        }
        if(huntTime < 0)
        {
            _Hunter.speed = 0;
        }
        if(spawnTime > 0)
        {
            return;
        }

        huntAnim.SetBool("CanAttack", false);
        huntAnim.SetBool("Patrol", true);
        huntAnim.SetBool("Chasing", false);

        range = Random.Range(5, 30);
        Move(patrolSpeed);
        if (_Hunter.remainingDistance <= _Hunter.stoppingDistance)
        {
            if (RandomPoint(centerPoint.position, range, out rayPoint))
            {
                Debug.DrawRay(rayPoint, Vector3.up, Color.blue, 1.0f);
                _Hunter.SetDestination(rayPoint);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void Move(float speed)
    {
        _Hunter.isStopped = false;
        _Hunter.speed = speed;
    }

    private void Stop()
    {
        huntAnim.SetBool("Patrol", false);
        huntAnim.SetBool("Chasing", false);

        _Hunter.isStopped = true;
        _Hunter.speed = 0;
    }
}
