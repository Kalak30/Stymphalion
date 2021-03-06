/*
 * Filename: QuestUI.cs
 * Developer: Jon Kopf
 * Purpose: Control automatic sizing of the Quest UI screen
 */

using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public GameObject m_background;
    public GameObject m_quest_text_prefab;
    public GameObject m_step_text_prefab;
    private List<Quest> m_displayed_quests;
    private int m_initial_scale = 1;
    private Rect m_prev_screen_size;
    private List<GameObject> m_quest_text_list;

    /// <summary>
    /// Adds a quest to the quest ui if it has not alreay been added.
    /// </summary>
    /// <param name="q"></param>
    public void AddQuest(Quest q)
    {
        if (m_displayed_quests.Contains(q) || q.m_quest_status != QuestStatus.active ) { return; }


        GameObject new_text = Instantiate(m_quest_text_prefab);
        new_text.transform.SetParent(m_background.transform);
        new_text.GetComponent<TextMeshProUGUI>().color = Color.red;
        

        new_text.GetComponent<TextMeshProUGUI>().text = q.m_quest_name;

        GameObject child = new_text.transform.GetChild(0).gameObject;
        child.GetComponent<TextMeshProUGUI>().text = q.m_quest_description;

        for(int i = 0; i < q.m_steps.Count; i++)
        {
            QuestStep s = q.m_steps[i];
            AddStep(s.m_step_name, s.m_step_description, new_text.transform);
        }


        m_quest_text_list.Add(new_text);
        m_displayed_quests.Add(q);
    }

    /// <summary>
    /// Used only for testing purposes. ScaleUI() is private. This is quite useless
    /// </summary>
    public void Test()
    {
        ScaleUI();
    }

    /// <summary>
    /// Toggles the display of the quest, and scales text if necessary
    /// </summary>
    public void ToggleDisplay()
    {

        if (m_background.activeSelf)
        {
            m_background.SetActive(false);
        }
        else
        {
            QuestManager.GetQuestManager().AddToUI();
            m_background.SetActive(true);
            PlaceText();
        }
    }

    /// <summary>
    /// Adds a new step to the text object list
    /// </summary>
    /// <param name="step_name"></param>
    /// <param name="step_description"></param>
    /// <param name="parent"></param>
    private void AddStep(string step_name, string step_description, Transform parent)
    {
        GameObject new_text = Instantiate(m_step_text_prefab);
        new_text.transform.SetParent(parent);

        new_text.GetComponent<TextMeshProUGUI>().richText = true;
        new_text.GetComponent<TextMeshProUGUI>().text = "-<line-indent=15%>" + step_name + "";
        GameObject child = new_text.transform.GetChild(0).gameObject;
        child.GetComponent<TextMeshProUGUI>().text = step_description;
    }

    /// <summary>
    /// Places the text of a step in relation to other text objects
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="local_height"></param>
    /// <returns></returns>
    private float PlaceStepText(GameObject obj, float local_height)
    {
        for (int i = 1; i < obj.transform.childCount; i++)
        {
            float pre_step_name_padding = 40f;
            float post_step_name_padding = 10f;

            RectTransform step_trans = obj.transform.GetChild(i).GetComponent<RectTransform>();
            RectTransform child_trans = step_trans.GetChild(0).GetComponent<RectTransform>();

            TextMeshProUGUI step_ugui = step_trans.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI child_ugui = child_trans.GetComponent<TextMeshProUGUI>();

            step_ugui.ForceMeshUpdate();
            child_ugui.ForceMeshUpdate();

            step_trans.localPosition = new Vector3(30, -local_height - pre_step_name_padding);
            child_trans.localPosition = new Vector3(10, -step_ugui.preferredHeight - post_step_name_padding);

            local_height += step_ugui.preferredHeight + pre_step_name_padding;

            if (step_trans.GetChild(0).GetComponent<TextMeshProUGUI>().text != "")
            {
                local_height += child_ugui.preferredHeight + post_step_name_padding;
            }
        }

        return local_height;
    }

    /// <summary>
    /// Scales the background in relation to the screen size. Has a minimum height and width 
    /// </summary>
    /// <param name="screen_size"></param>
    private void ScaleBackground(Rect screen_size)
    {
        int MIN_WIDTH = 800;
        int MIN_HEIGHT = 200;

        RectTransform background_trans = m_background.GetComponent<RectTransform>();

        float new_width = screen_size.width * (0.80f);
        float new_height = screen_size.height * (0.65f);

        if (new_width < MIN_WIDTH)
        {
            new_width = MIN_WIDTH;
        }

        if (new_height < MIN_HEIGHT)
        {
            new_height = MIN_HEIGHT;
        }

        background_trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, new_width);
        background_trans.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, new_height);
    }

    /// <summary>
    /// Places the text on the screen based on the screen size
    /// </summary>
    private void PlaceText()
    {
        RectTransform background_trans = m_background.GetComponent<RectTransform>();

        float top_margin = 75;
        float left_margin = 35;
        int column = 0;
        float y_start = (background_trans.rect.height / 2) - top_margin;
        float x_start = -(background_trans.rect.width / 2) + left_margin;

        float bottom_padding = 35;
        float total_height = 0;
        float total_width = 0;

        foreach (GameObject obj in m_quest_text_list)
        {
            if (total_width > background_trans.rect.width - left_margin)
            {
                throw new UnityException("The width of Quest UI text is too large");
            }

            RectTransform obj_trans = obj.GetComponent<RectTransform>();
            RectTransform child_trans = obj_trans.GetChild(0).GetComponent<RectTransform>();

            TextMeshProUGUI obj_ugui = obj_trans.GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI child_ugui = child_trans.GetComponent<TextMeshProUGUI>();

            obj_ugui.ForceMeshUpdate();
            child_ugui.ForceMeshUpdate();

            float local_height = 0;
            float x = x_start + total_width + obj_trans.rect.width / 2;
            float y = y_start - total_height - obj_trans.rect.height / 2;

            obj_trans.localPosition = new Vector3(x, y, 0);
            child_trans.localPosition = new Vector3(0, -obj_ugui.preferredHeight, -obj_trans.rect.height);
            local_height = obj_ugui.preferredHeight + child_ugui.preferredHeight;
            local_height = PlaceStepText(obj, local_height);

            total_height += local_height + bottom_padding;

            if (total_height + 100 > background_trans.rect.height)
            {
                total_width = obj_ugui.preferredWidth * ++column;
                total_height = 0;
            }
            if (total_width > background_trans.rect.width - left_margin)
            {
                return;
            }
        }
    }

    /// <summary>
    /// Scales the entire UI
    /// </summary>
    private void ScaleUI()
    {
        // No need to scale if there has not been a screen size update
        Rect screen_size = new Rect(0, 0, Screen.width, Screen.height);
        if (m_prev_screen_size.Equals(screen_size) && m_initial_scale != 1)
        {
            return;
        }

        m_initial_scale = 0;

        ScaleBackground(screen_size);
        PlaceText();
    }

    /// <summary>
    /// On startup, initialize a few variables
    /// </summary>
    private void Start()
    {
        m_quest_text_list = new List<GameObject>(GameObject.FindGameObjectsWithTag("QuestText"));
        m_prev_screen_size = new Rect(0, 0, Screen.width, Screen.height);
        m_displayed_quests = new List<Quest>();
    }

    // Update is called once per frame
    private void Update()
    {
        ScaleUI();
    }
}