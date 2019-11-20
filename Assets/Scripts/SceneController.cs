using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void MoveToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void MoveToGame()
    {
        SceneManager.LoadScene("World");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
