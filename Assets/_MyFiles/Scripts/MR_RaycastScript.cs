using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_RaycastScript : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    RaycastHit hitInfo;
    [SerializeField] MR_TimerCountDownScript timerCountdown;

    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        if (Physics.Raycast(ray, out hitInfo, 20f, layerMask))
        {
            Debug.Log("Hit something");
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.cyan);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hitInfo.transform.gameObject);
                timerCountdown.MaxTimeChange(10);
            }
        }
        else
        {
            Debug.Log("Hit nothing");
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.red);
        }
    }
}
