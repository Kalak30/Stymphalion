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
            GameObject hydra = GameObject.Find("Hydra");
            GameObject ply = GameObject.Find("Player");
            GameObject UI = GameObject.Find("UI");
            //
            /*            
                        Scene dontdes = SceneManager.GetSceneAt(1);
                        var x = dontdes.GetRootGameObjects();
                        foreach (var obj in x){
                            if(obj.name == "UI"){
                                Destroy(obj);
                            }
                        }
            */
            Destroy(UI);
            Destroy(hydra);
            Destroy(ply);
            SceneManager.LoadScene("MainIsland");
            
            
        }

        
        player.SetRight();
        //player.m_location

       // Debug.Log("Baby your a firework");
    }

}
