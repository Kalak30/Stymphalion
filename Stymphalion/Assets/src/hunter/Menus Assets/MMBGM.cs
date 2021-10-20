/*
* Filename: MMBGM.cs
* Developer: Hunter Leppek
* Purpose: This file implements control of the main menu background music (MMBGM)
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// A class that implements control of the MMGBM
/// Member Variables
/// <list type = "bullet">
/// <item>m_volume_slider</item>
/// </list>
/// </summary>
public class MMBGM : MonoBehaviour
{
    [SerializeField] Slider m_volume_slider;
    /// <summary>
    /// Initializes the volume slider
    /// </summary>
    void Start()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    /// <summary>
    /// Changes the volume in accordance to the value of the volume slider
    /// </summary>
    public void changeVolume()
    {
        AudioListener.volume = m_volume_slider.value;
        Save();
    }

    /// <summary>
    /// Saves the value of the volume slider so that the player does not have to set the volume everytime they enter the game
    /// </summary>
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", m_volume_slider.value);
    }

    /// <summary>
    /// Loads the saved value of the volume slider
    /// </summary>
    private void Load()
    {
        m_volume_slider.value = PlayerPrefs.GetFloat("musicVolume");
    }
}