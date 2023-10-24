using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MR_EndGameScript : MonoBehaviour
{
    GameObject[] items;
    [SerializeField] GameObject notDone;
    [SerializeField] GameObject gameWon;

    [SerializeField] MR_PlayerScript player;
    [SerializeField] AudioSource audioSource;

    private void Awake()
    {
        notDone.SetActive(false);
        gameWon.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        items = GameObject.FindGameObjectsWithTag("Collectable");

        if (other.CompareTag("Player"))
        {
            if(items.Length > 0)
            {
                notDone.SetActive(true);
            }
            else if (items.Length <= 0)
            {
                EndGame();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            notDone.SetActive(false);
        }
    }

    private void EndGame()
    {
        audioSource.Stop();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        gameWon.SetActive(true);
        player.GameWon(true);
        Time.timeScale = 0;
    }
}
