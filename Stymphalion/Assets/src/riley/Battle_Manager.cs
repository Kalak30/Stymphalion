using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using trevor

EnemyAI enemyAction;

public class Battle_Manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        enemyAction = gameObject.AddComponent<EnemyAI>()as EnemyAI;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void initializeBattle(){

        Debug.Log("Battle Manager not yet set up");
        Debug.Log("Battle Manager will call function from AI shown below");
        enemyAction.EnemyMove();

    }
}
