using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyNPC : EnvironmentObjectSuperClass
{
    private Battle_Manager battleManager;
    // Start is called before the first frame update

    public override void interactFunc(){
        battleManager.initializeBattle();
    }

    void Start()
    {
        battleManager = gameObject.AddComponent<Battle_Manager>() as Battle_Manager;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
