using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MR_TimerCountDownScript : MonoBehaviour
{
    public TextMeshProUGUI timerDisplay;

    public int seconds = 30;
    public int minutes = 1;
    public bool takeSecond = false;

    private void Awake()
    {
        while (seconds >= 60)
        {
            seconds -= 60;
            minutes += 1;
        }
    }

    private void Start()
    {
        if (seconds < 10)
        {
            timerDisplay.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timerDisplay.text = minutes.ToString() + ":" + seconds.ToString();
        }
    }

    IEnumerator TimerTake()
    {
        takeSecond = true;

        yield return new WaitForSeconds(1);
        seconds -= 1;

        if (seconds == 0 && minutes > 0)
        {
            seconds = 60;
            minutes -= 1;
        }

        if (seconds < 10)
        {
            timerDisplay.text = minutes.ToString() + ":0" + seconds.ToString();
        }
        else
        {
            timerDisplay.text = minutes.ToString() + ":" + seconds.ToString();
        }

        takeSecond = false;
    }

}
