using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_RoomTimeScript : MonoBehaviour
{
    MR_TimerCountDownScript timerCountdown;

    private void Start()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("I enter room");

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I exit room");
        }
    }


}
