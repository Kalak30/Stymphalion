using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A decorator class for the healthbar that "decorates" the healthbar color
/// /// Member Variables
/// <list type = "bullet">
/// <item>m_fill</item>
/// <item>m_health_slider</item>
/// <item>m_gradient</item>
/// </summary>
public class HealthColor : MonoBehaviour
{
    public Image m_fill;
    public Slider m_health_slider;
    public Gradient m_gradient;

    /// <summary>
    /// Updates the color of the healthbar to indicate the status of the player. (i.e. green = healthy, yellow = damaged, red = near death)
    /// </summary>
    void Update()
    {
        m_fill.color = m_gradient.Evaluate(m_health_slider.normalizedValue);
    }
}
