using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_Attack_Script : MonoBehaviour
{
    public void AttackPoint()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + Vector3.up * 1 + transform.forward, 1.5f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject == gameObject)
            {
                continue;
            }
            MR_PlayerScript playerScript = collider.GetComponent<MR_PlayerScript>();
            if (playerScript != null)
            {
                playerScript.HealthChange(-50);
            }
        }
    }
}
