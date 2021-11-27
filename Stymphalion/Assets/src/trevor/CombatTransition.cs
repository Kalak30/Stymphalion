using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CombatTransition : MonoBehaviour
{
    public DemoMode m_Demo;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnColliderEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Player touched enemy, starting combat");
            m_Demo.testClass.PauseReplay();
            
            SceneManager.LoadScene("CombatSceneDemo");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Debug.Log("Touched");
            m_Demo.testClass.PauseReplay();

            SceneManager.LoadScene("CombatSceneDemo");
        }
    }
}
