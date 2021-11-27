using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEngine.SceneManagement;

public class EnemyValuesTest
{
    [UnityTest]
    public IEnumerator Values()
    {
        SceneManager.LoadScene("CombatSceneDemo");
        yield return new WaitForSeconds(2);
        GameObject battle_System = GameObject.Find("BattleSystem");
        GameObject player = GameObject.Find("Player");
        GameObject enemy = GameObject.Find("Enemy");
        

        
    }

}
