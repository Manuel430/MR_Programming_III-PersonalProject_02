using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class MR_TimerCountDownScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float maxTime;
    [SerializeField] float remainingTime;
    [SerializeField] bool beginCountdown;
    [SerializeField] bool hunterActive;

    [SerializeField] MR_HunterSpawnScript hunterSpawn;

    private void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (beginCountdown)
        {
            if (remainingTime > 1 && hunterActive == false)
            {
                remainingTime -= Time.deltaTime;
                timerText.color = Color.white;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                timerText.color = Color.red;
                if (hunterActive == true)
                {
                    Debug.LogWarning("The Hunter is on the Hunt.");
                    hunterSpawn.HunterSpawn();
                    hunterActive = false;
                }
            }
        }
        else
        {
            if(hunterActive == false)
            {
                remainingTime = maxTime;
            }
        }

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void BoolSwitch(bool CountDown)
    {
        beginCountdown = CountDown;
    }

    public void MaxTimeChange(float maxTimeChange)
    {
        maxTime -= maxTimeChange;
        SetNewTime();
    }

    public void SetNewTime()
    {
        remainingTime = maxTime;
    }
}
