using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_HuntingRadiusScript : MonoBehaviour
{
    [SerializeField] MR_HunterScript hunterRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunterRadius.CanChase(true);
            hunterRadius.CanPatrol(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunterRadius.CanChase(false);
            hunterRadius.CanPatrol(true);
        }
    }
}
