/*
* Filename: MainMenu.cs
* Developer: Hunter Leppek
* Purpose: This file implements a main menu for the game
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// A main menu class for the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// A function to change the scene to the game once the main menu play button is pressed.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /// <summary>
    /// A function to quit the game once the main menu quit button is pressed.
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("QUIT"); //Used to confirm that the program will quit, despite it not doing so in a development environment
        Application.Quit();
    }
}
