/*
* Filename: HealthBar.cs
* Developer: Hunter Leppek
* Purpose: This file implements a health bar for the main character, referred to here as "mc." 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// A healthbar class for the mc
/// Two other classes: HealthBorder.cs and HealthColor.cs help implement the healthbar using a decorator pattern.
/// Member Variables
/// <list type = "bullet">
/// <item>m_health_slider</item>
/// <item>m_fill</item>
/// <item>m_health_sprite</item>
/// <item>m_mc</item>
/// </list>
/// </summary>
public class HealthBar : MonoBehaviour
{
    public Slider m_health_slider;
    public Image m_fill;
    public Image m_health_sprite;
    PlayerClass m_mc;

    /// <summary>
    /// Initializes the player health to 100
    /// </summary>
    void Start()
    {
        m_mc = PlayerClass.Instance;
        SetMaxHealth(100);
    }

    /// <summary>
    /// Checks the player health every frame and changes the healthbar to reflect that health
    /// </summary>
    void Update() 
    {
        if (m_mc.m_health > 100)
        {
            m_mc.m_health = 100;
        }
        if (m_mc.m_health < 0)
        {
            m_mc.m_health = 0;
        }
        
        SetHealth(m_mc.m_health);

    }

    /// <summary>
    /// Sets the max health of the player to the passed health
    /// </summary>
    /// <param name="health"></param>
    private void SetMaxHealth(int health)
    {
        m_health_slider.maxValue = health;
        m_health_slider.value = health;
    }

    /// <summary>
    /// Sets the health of the player to the passed health
    /// </summary>
    /// <param name="health"></param>
    private void SetHealth(int health)
    {
        m_health_slider.value = health;
    }


}
