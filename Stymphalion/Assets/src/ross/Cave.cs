using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cave : EnvirmentObjectSuperClass
{
 
    public override void InteractFunc()
    {
        Scene CurrentScence = SceneManager.GetActiveScene();

        if (CurrentScence.name == "MainIsland")
        {
            SceneManager.LoadScene("HydraCave");
        }
        else{
            SceneManager.LoadScene("MainIsland");
        }

        PlayerClass player = PlayerClass.Instance;
        player.SetRight();
        //player.m_location

        Debug.Log("Baby your a firework");
    }

}
