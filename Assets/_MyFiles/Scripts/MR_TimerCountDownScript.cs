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
    [SerializeField] int hunterActive = 1;
    [SerializeField] int locChoice = 1;

    [SerializeField] MR_HunterSpawnScript hunterSpawn;
    [SerializeField] GameObject[] spawnerLoc;

    private void Update()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);

        if (beginCountdown)
        {
            if (remainingTime > 1 && hunterActive == 1)
            {
                remainingTime -= Time.deltaTime;
                timerText.color = Color.white;
            }
            else if (remainingTime < 1)
            {
                remainingTime = 0;
                timerText.color = Color.red;
                if (hunterActive == 1)
                {
                    Debug.LogWarning("The Hunter is on the Hunt.");
                    if(locChoice == 1)
                    {
                        LocationList();
                        locChoice = 0;
                    }
                    hunterSpawn.HunterSpawn();
                    hunterActive = 0;
                }
            }
        }
        else
        {
            if(hunterActive == 1)
            {
                remainingTime = maxTime;
            }
        }

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void LocationList()
    {
        int randomIndex = Random.Range(0, spawnerLoc.Length);
        Debug.Log($"Hunter has spawned at location {randomIndex}");
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
