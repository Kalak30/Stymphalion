using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    //Main Character (the player)
    public PlayerClass MC; 
    //Sets the max health
    private void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    //Sets current health
    private void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    //Player health starts at 100
    void Start()
    {
        SetMaxHealth(100);
    }

    void Update() //Check player heath and update it
    {
        //Always check inputs before doing anything with them: software security 101
        if (MC.health > 100)
        {
            MC.health = 100;
        }
        if (MC.health < 0)
        {
            MC.health = 0;
        }
        
        SetHealth(MC.health);
    }
}
