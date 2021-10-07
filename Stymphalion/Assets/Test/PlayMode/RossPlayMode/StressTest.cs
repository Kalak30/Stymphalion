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
    public IEnumerator MapTest(){
        SceneManager.LoadScene("MainIsland");
        // wiat for scene to load
        yield return new WaitForSeconds(2);

        Debug.Log("XXX");
        

        PlayerClass player = GameObject.Find("Player").GetComponent<PlayerClass>();
       // Rigidbody2D player = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        player.movementSpeed = 5;
        Debug.Log("Lil WAYNE");

        while((player.location.x < 55) && (player.location.x > -41) && (player.location.y < 37) && (player.location.y > -35)){
            player.movementSpeed = player.movementSpeed * 1.1f;

            yield return new WaitForSeconds(1);
        }
    
        Debug.Log("Final Player Speed:" + player.movementSpeed);
        Debug.Log("Final Player Location: x: " + player.location.x + "    y: " + player.location.y);
        Assert.IsTrue((player.location.x > 55) | (player.location.x < -41) | (player.location.y > 37) | (player.location.y < -35) );
        
        
    }




}