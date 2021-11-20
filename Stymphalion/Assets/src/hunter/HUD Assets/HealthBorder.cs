using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A decorator class for the healthbar that "decorates" the healthbar border
/// /// Member Variables
/// <list type = "bullet">
/// <item>m_health_sprite</item>
/// <item>m_death_sprite</item>
/// <item>m_mc</item>
/// </summary>
public class HealthBorder : MonoBehaviour
{
    public Image m_health_sprite;
    public Image m_death_sprite;
    PlayerClass m_mc;

    /// <summary>
    /// Hides the death image and shows the healthbar. Also begins keeping track of player health.
    /// </summary>
    void Start()
    {
        m_health_sprite.enabled = true;
        m_death_sprite.enabled = false;
        m_mc = PlayerClass.Instance;
    }

    /// <summary>
    /// When the player's health goes to 0, change replace the health bar with a skull, indicating that the player is dead.
    /// </summary>
    void Update()
    {
        if (m_mc.m_health <= 0)
        {
            m_health_sprite.enabled = false;
            m_death_sprite.enabled = true;
        }
    }
}
