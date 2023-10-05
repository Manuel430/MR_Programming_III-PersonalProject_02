using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MR_RoomTimeScript : MonoBehaviour
{
    MR_TimerCountDownScript timerCountdown;
    public GameObject barrier;
    public int secRestart;
    public int minRestart;

    private void Start()
    {
        timerCountdown = GetComponent<MR_TimerCountDownScript>();
    }

}
