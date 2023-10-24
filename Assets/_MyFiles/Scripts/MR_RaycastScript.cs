using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MR_RaycastScript : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    RaycastHit hitInfo;
    [SerializeField] MR_TimerCountDownScript timerCountdown;
    [SerializeField] GameObject collectingUI;
    GameObject[] collectingObjects;
    [SerializeField] GameObject allItemsHad;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip itemCollect;
    [SerializeField] AudioClip allItemsCollect;


    private void Update()
    {
        Ray ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward));

        collectingObjects = GameObject.FindGameObjectsWithTag("Collectable");
        if(collectingObjects.Length <= 1)
        {
            audioSource.clip = allItemsCollect;
        }
        else
        {
            audioSource.clip = itemCollect;
        }


        if (Physics.Raycast(ray, out hitInfo, 20f, layerMask))
        {
            Debug.DrawRay(transform.position,transform.TransformDirection(Vector3.forward) * hitInfo.distance, Color.cyan);
            collectingUI.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(hitInfo.transform.gameObject);
                timerCountdown.MaxTimeChange(10);

                audioSource.Play();
            }
        }
        else
        {
            collectingUI.SetActive(false);
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20f, Color.red);
        }

        if(collectingObjects.Length == 0)
        {
            allItemsHad.SetActive(true) ;
        }
    }
}
