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
    }

    /*    public TextMeshProUGUI timerDisplay;

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
        }*/

}
