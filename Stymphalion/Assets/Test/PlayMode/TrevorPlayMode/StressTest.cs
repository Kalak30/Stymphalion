using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEditor;
using UnityEditor.SceneManagement;

public class StressTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public int testvalue = 1;
    public int testvalue2 = 60;
    [UnityTest]
    public IEnumerator Test3()
    {
        EditorSceneManager.LoadScene("MainIsland");
        yield return new WaitForSeconds(3);
        while (testvalue2 > 0)
        {
            //testvalue = testvalue + testvalue;
            Debug.Log(testvalue);
            testvalue2++;
            //yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
