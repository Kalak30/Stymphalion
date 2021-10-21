/*
*
* Filename: EnemeyNPC.cs
* Developer: Ross Prestwich
* Purpose: For Starting Fight ---- Subclass of EnvirmentObjectSuperClass.cs
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyNPC : EnvirmentObjectSuperClass
{
    private BattleManager m_battle_manager;


    /// <summary>
    /// Implements the interact function for enemey NPC's
    /// </summary>
    public override void InteractFunc(){
        m_battle_manager.InitializeBattle();
    }

/// <summary>
/// initalize battle manager to start battle
/// </summary>
    void Start()
    {
        m_battle_manager = gameObject.AddComponent<BattleManager>() as BattleManager;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
