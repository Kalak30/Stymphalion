/*
 * Filename: QuestNode.cs
 * Developer: Jon Kopf
 * Purpose: Provide a unique description of a quest node within the quest editor
 */

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// A characterization of a Quest node, a subclass of Node, within the quest editor
/// <para>Member Variables</para>
/// <list type="bullet">
///     <item>m_data</item>
///     <item>m_item_type</item>
///     <item>m_quest_status</item>
/// </list>
/// </summary>
public class QuestNode : Node
{
    public QuestData m_data;

    private Item.ItemType m_item_type;
    private QuestStatus m_quest_status;

    /// <summary>
    ///
    /// </summary>
    /// <param name="position">Position of new QuestNode window</param>
    /// <param name="width">width of new QuestNode window</param>
    /// <param name="height">height of new QuestNode window</param>
    /// <param name="id">Unique identifier for QuestNode, should be different from all other Nodes</param>
    /// <param name="node_style">Graphical style of new QuestNode</param>
    /// <param name="selected_style">Graphical style of QuestNode when selected</param>
    /// <param name="in_point_style">Graphical style of the in point Connection points</param>
    /// <param name="out_point_style">Graphical style of the out Connection points</param>
    /// <param name="OnClickInPoint">Action to take when an in point is clicked</param>
    /// <param name="OnClickOutPoint">Action to take when an out point is clicked</param>
    /// <param name="OnClickRemoveNode">Action to take when the remove node button is clicked</param>
    public QuestNode(Vector2 position, float width, float height, int id,
                    GUIStyle node_style, GUIStyle selected_style, GUIStyle in_point_style, GUIStyle out_point_style,
                    Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) :
           base(position, width, height, id, node_style, selected_style, in_point_style, out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
        m_type = NodeType.Quest;
        m_title = "Quest";

        m_out_points = new List<ConnectionPoint>
        {
            new ConnectionPoint(this, 0,  "Quest", 7.5f, ConnectionPointType.Out, out_point_style, OnClickOutPoint),
            new ConnectionPoint(this, 1, "Next", 35f, ConnectionPointType.Out, out_point_style, OnClickOutPoint)
        };

        m_allowed_inputs = new List<ConnectionRule>
        {
        };
        m_allowed_outputs = new List<ConnectionRule>
        {
            new ConnectionRule(NodeType.Quest, 0, NodeType.Step, 0)
        };
    }

    /// <summary>
    /// Provides the layout within the QuestNode
    /// </summary>
    /// <param name="id"></param>
    public override void DrawWindow(int id)
    {
        string questName;
        string questDescription;

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.BeginVertical();
        //EditorGUILayout.LabelField("Quest Giver");
        EditorGUILayout.LabelField("Quest Name");
        EditorGUILayout.LabelField("Quest Description");
        EditorGUILayout.LabelField("Quest Reward");
        EditorGUILayout.LabelField("Quest Status");
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical();
        //UnityEngine.Object questOwner = EditorGUILayout.ObjectField(Data, typeof(Quest_NPC), false);
        questName = EditorGUILayout.TextField(m_data.m_quest_name);
        questDescription = EditorGUILayout.TextArea(m_data.m_quest_description);
        m_item_type = (Item.ItemType)EditorGUILayout.EnumPopup(m_data.m_quest_reward);
        m_quest_status = (QuestStatus)EditorGUILayout.EnumPopup(m_data.m_quest_status);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(20);

        EditorGUILayout.EndHorizontal();

        m_data.m_quest_name = questName;
        m_data.m_quest_description = questDescription;
        m_data.m_quest_reward = m_item_type; //itemType;
        m_data.m_quest_status = m_quest_status;
    }
}