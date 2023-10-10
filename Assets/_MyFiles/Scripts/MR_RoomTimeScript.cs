using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_RoomTimeScript : MonoBehaviour
{
    [SerializeField] MR_TimerCountDownScript timerCountdown;

/*    private void Awake()
    {
        if(timerCountdown == null)
        {
            timerCountdown = th
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("I enter room");

            timerCountdown.BoolSwitch(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I exit room");

            timerCountdown.BoolSwitch(false);
        }
    }


}
