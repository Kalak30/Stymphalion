using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
    public int max_Health = 100;
    public int current_Health = 100;
    public int enemy_Health;
    public float enemy_Health_Percentage;

    public EnemyAIDemo enemyData;
    //int behavior = Random.Range(0, 3);

    // Start is called before the first frame update
    void Start()
    {
        current_Health = max_Health;
        enemy_Health = enemyData.current_Health;
        enemy_Health_Percentage = enemy_Health / enemyData.max_Health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int PlayerMove()
    {
        return 0;
    }

    public void CalculateDamage()
    {

    }
}
