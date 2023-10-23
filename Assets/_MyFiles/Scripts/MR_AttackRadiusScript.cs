using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_AttackRadiusScript : MonoBehaviour
{
    [SerializeField] MR_HunterScript hunterRange;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunterRange.CanAttack(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hunterRange.CanAttack(false);
        }
    }
}
