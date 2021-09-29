using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void playGame()
    {
        //Need to establish game scene first, otherwise this does nothing
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
