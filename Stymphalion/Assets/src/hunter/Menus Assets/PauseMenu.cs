/*
* Filename: PauseMenu.cs
* Developer: Hunter Leppek
* Purpose: This file implements a pause menu for the game. 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A pause menu class for the pause menu. This is a subclass of the main menu class so 
/// that the pause menu can use the QuitGame() function from the main menu superclass.
/// </summary>
public class PauseMenu : MainMenu
{
    public static bool m_is_paused = false;
    public GameObject m_pause_menu_UI;

    /// <summary>
    /// A function to resume the game once the pause menu resume button is pressed.
    /// This function is an overriding function because the main menu and the pause menu function differ
    /// The main menu PlayGame changes scene, whereas the pause menu play game resumes time and hides itself.
    /// This is a case of dynamic binding.
    /// </summary>
    public override void PlayGame()
    {
        m_pause_menu_UI.SetActive(false);
        Time.timeScale = 1f;
        m_is_paused = false;
    }

    /// <summary>
    /// A function to pause the game
    /// </summary>
    void Pause()
    {
        m_pause_menu_UI.SetActive(true);
        Time.timeScale = 0f;
        m_is_paused = true;
    }
    
    /// <summary>
    /// This functions allows the pause menu to be opened in game by pressing the "escape" button.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_is_paused)
            {
                PlayGame();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// This function resumes time and changes the scene to the main menu
    /// </summary>
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("src/hunter/Menus Assets/Main Menu");
    }
}
