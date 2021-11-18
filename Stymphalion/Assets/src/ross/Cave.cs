using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cave : EnvirmentObjectSuperClass
{
 
    /// <summary>
    /// Interact Func for cave object
    /// </summary>
    public override void InteractFunc()
    {
        Scene CurrentScence = SceneManager.GetActiveScene();
        PlayerClass player = PlayerClass.Instance;

        if (CurrentScence.name == "MainIsland")
        {
            player.m_new_scene_player_location = new Vector2(4, 2);
            SceneManager.LoadScene("HydraCave");
        }
        else{
            player.m_new_scene_player_location = new Vector2(21, 11);
            SceneManager.LoadScene("MainIsland");
            
        }

        
        player.SetRight();
        //player.m_location

       // Debug.Log("Baby your a firework");
    }

}
