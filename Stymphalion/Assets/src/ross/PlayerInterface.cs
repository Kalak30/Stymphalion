using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerClass m_player_class;

    [SerializeField] private UI_Inventory uiInventory;

    void Awake()
    { 
        m_player_class = PlayerClass.Instance ;
    }

    void Start(){
        m_player_class.InitVariables(uiInventory);
        m_player_class.OnEnable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_player_class.FixedUpdate();
    }

    void OnCollisionStay2D(Collision2D other){

        if ("enviromentObject" == other.gameObject.tag)
        {
          //  Debug.Log("test");
            m_player_class.OnCollisionStay2D(other);
        }

    }

    void OnTriggerStay2D(Collider2D other){
        if ("enviromentObject" == other.gameObject.tag)
        {
         //   Debug.Log("test");
            m_player_class.OnTriggerStay2D(other);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        m_player_class.OnTriggerEnter2D(other);
    }

}
