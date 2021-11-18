using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIDemo : MonoBehaviour
{
    public int max_Health;
    public int current_Health;
    public int player_Health;
    public float player_Health_Percentage;

    public PlayerAI playerData;

    // Start is called before the first frame update
    void Start()
    {
        max_Health = Random.Range(99, 301);
        current_Health = max_Health;
    }

    // Update is called once per frame
    void Update()
    {
        //GetPlayerData();
    }

    private void FixedUpdate()
    {
        GetPlayerData();


    }

    public void GetPlayerData()
    {
        player_Health = playerData.current_Health;
        player_Health_Percentage = player_Health / playerData.max_Health;
    }

    public void CreateEnemyData()
    {
        //Enemy health + move list
    }

    public int EnemyMove()
    {
        //Pick a move based on criteria and send it's data to BattleSystem
        return 0;
    }

    public void CalculateDamage(int x)
    {
        //Change enemy health
    }
}
