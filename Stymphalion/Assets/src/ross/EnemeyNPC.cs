using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyNPC : EnvirmentObjectSuperClass
{
    private Battle_Manager battleManager;
    // Start is called before the first frame update


    // Dynamic Binding lets gooo
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
