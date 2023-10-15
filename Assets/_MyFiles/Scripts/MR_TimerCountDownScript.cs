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

    private void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (beginCountdown)
        {
            if (remainingTime > 1)
            {
                remainingTime -= Time.deltaTime;
                timerText.color = Color.white;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                //EnemySpawn
                timerText.color = Color.red;
            }
        }
        else
        {
            remainingTime = maxTime;
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
