using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
public class BattleSystem : MonoBehaviour
{

    public PlayerAI playerData;
    public EnemyAIDemo enemyData;
    private UnitDataClass testData;

    // Start is called before the first frame update
    void Start()
    {
        
        enemyData.GetPlayerData();
    }

    // Update is called once per frame
    void Update()
    {
        //enemyData.GetPlayerData();
    }
}
