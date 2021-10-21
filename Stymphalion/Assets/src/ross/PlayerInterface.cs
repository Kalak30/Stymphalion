using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerClass m_player_class;

    void Awake()
    {
      m_player_class = PlayerClass.Instance ;
        m_player_class.Awake();
        m_player_class.OnEnable();


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        m_player_class.FixedUpdate();
    }
}
