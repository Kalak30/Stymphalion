using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

/*public class testPlayerClass : PlayerClass{
    private Rigidbody2D player;
    Vector2 newVelo = new Vector2(1,1);
    public override void Movement(){
        player.velocity = newVelo = newVelo * 2;
    }


}
*/

public class MapColiderStressTest
{
    [UnityTest]
    public IEnumerator MapTest()
    {
        SceneManager.LoadScene("MainIsland");
        // wiat for scene to load
        yield return new WaitForSeconds(2);

        Debug.Log("XXX");
        

        PlayerClass player = GameObject.Find("Player").GetComponent<PlayerClass>();
       // Rigidbody2D player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player.m_movement_speed = 5;
        Debug.Log("Lil WAYNE");

        while((player.m_location.x < 55) && (player.m_location.x > -41) && (player.m_location.y < 37) && (player.m_location.y > -35)){
            player.m_movement_speed = player.m_movement_speed * 1.1f;

            yield return new WaitForSeconds(1);
        }
    
        Debug.Log("Final Player Speed:" + player.m_movement_speed);
        Debug.Log("Final Player Location: x: " + player.m_location.x + "    y: " + player.m_location.y);
        Assert.IsTrue((player.m_location.x > 55) | (player.m_location.x < -41) | (player.m_location.y > 37) | (player.m_location.y < -35) );
        
        
    }




}