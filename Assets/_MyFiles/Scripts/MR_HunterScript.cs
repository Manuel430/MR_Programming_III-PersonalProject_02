using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    }

    private void Update()
    {
        Patrol();

        /*        if (onPatrol)
                {
                    Patrol();
                    Debug.Log("Patrolling");
                }
                else
                {
                    Chase();
                    Debug.Log("Chasing");
                }*/
    }

    private void Chase()
    {
    }

    private void Patrol()
    {
        range = Random.Range(5, 30);
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
}
