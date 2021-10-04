using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Test : MonoBehaviour
{
    private Quest_Manager qm;

    // Start is called before the first frame update
    private void Start()
    {
        qm = gameObject.AddComponent<Quest_Manager>();

        for (int i = 0; i < 200; i++)
        {
            Quest q = new Quest(i.ToString(), i.ToString(), "money!!!");

            for (int j = 0; j < 5; j++)
            {
                q.AddStep(j, (i + j).ToString(), (i - j).ToString());
            }
            qm.AddQuest(i, q);
        }

        qm.DisplayQuests();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}