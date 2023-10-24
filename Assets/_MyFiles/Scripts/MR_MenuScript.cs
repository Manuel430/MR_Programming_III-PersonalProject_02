using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MR_MenuScript : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void PlayMainGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene(1);
    }

    public void TitleMenu()
    {
        SceneManager.LoadScene(0);
    }
}
