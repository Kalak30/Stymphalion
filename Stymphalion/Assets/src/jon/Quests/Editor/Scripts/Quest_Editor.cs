using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Quest_Editor : EditorWindow
{
    public QuestsListData m_quests_lists_data;
    public List<StepData> m_unassigned_steps;

    [MenuItem("QUESTS/Quest Editor")]
    public static void OpenWindow()
    {
        Quest_Editor wnd = GetWindow<Quest_Editor>();
        wnd.titleContent = new GUIContent("Quest Editor");
    }

    private List<Connection> m_connections;
    private Vector2 m_drag;
    private GUIStyle m_in_point_style;
    private int m_next_node_id;
    private GUIStyle m_node_style;
    private List<Node> m_nodes;
    private Vector2 m_offset;
    private GUIStyle m_out_point_style;
    private List<Quest_Node> m_quest_nodes;
    private ConnectionPoint m_selected_in_point;
    private GUIStyle m_selected_node_style;
    private ConnectionPoint m_selected_out_point;
    private List<Step_Node> m_step_nodes;

    private void ClearConnectionSelection()
    {
        m_selected_in_point = null;
        m_selected_out_point = null;
    }

    //Super ugly, way too many if statements. Try to re-factor
    private void CreateConnection()
    {
        if (m_connections == null)
        {
            m_connections = new List<Connection>();
        }
        if (m_selected_in_point.m_node.CanConnect(m_selected_in_point.m_type, m_selected_in_point.m_id, m_selected_out_point.m_node.m_type, m_selected_out_point.m_type, m_selected_out_point.m_id))
        {
            // Connection must be crated first, then data can be adjusted based on new connection
            m_connections.Add(new Connection(m_selected_in_point, m_selected_out_point, OnClickRemoveConnection));

            if (m_selected_in_point.m_node.m_type == NodeType.Step && m_selected_in_point.m_id == 0 && m_selected_out_point.m_id == 0)
            {
                Debug.Log("Here");
                Quest_Node qn = GetRootQuest(m_selected_in_point.m_node);
                if (qn != null)
                {
                    qn.Data.m_steps.Add(((Step_Node)m_selected_in_point.m_node).m_data);
                    m_unassigned_steps.Remove(((Step_Node)m_selected_in_point.m_node).m_data);
                }
            }
        }
        else
        {
            ClearConnectionSelection();
        }
    }

    private void DrawConnectionLine(Event e)
    {
        if (m_selected_in_point != null && m_selected_out_point == null)
        {
            Handles.DrawBezier(
                m_selected_in_point.m_rect.center,
                e.mousePosition,
                m_selected_in_point.m_rect.center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (m_selected_out_point != null && m_selected_in_point == null)
        {
            Handles.DrawBezier(
                m_selected_out_point.m_rect.center,
                e.mousePosition,
                m_selected_out_point.m_rect.center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    private void DrawConnections()
    {
        if (m_connections != null)
        {
            foreach (Connection c in m_connections)
            {
                c.Draw();
            }
        }
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        m_offset += m_drag * 0.5f;
        Vector3 newOffset = new Vector3(m_offset.x % gridSpacing, m_offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes()
    {
        BeginWindows();

        if (m_nodes != null)
        {
            foreach (Node n in m_nodes)
            {
                Quest_Node questNode = GetQuestNode(n.m_id);
                Step_Node stepNode = GetStepNode(n.m_id);

                if (questNode != null)
                {
                    n.m_rect = GUI.Window(n.m_id, n.m_rect, questNode.DrawWindow, n.m_title);
                }

                if (stepNode != null)
                {
                    n.m_rect = GUI.Window(n.m_id, n.m_rect, stepNode.DrawWindow, n.m_title);
                }

                n.DrawConnectionPoints();
            }
        }

        EndWindows();
    }

    private List<Connection> GetConnections(ConnectionPoint p)
    {
        List<Connection> connections = new List<Connection>();
        foreach (Connection c in m_connections)
        {
            if (c.m_in_point == p || c.m_out_point == p)
            {
                connections.Add(c);
            }
        }

        return connections;
    }

    private Quest_Node GetQuestNode(int id)
    {
        if (m_quest_nodes is null) return null;
        foreach (var node in m_quest_nodes)
        {
            if (node.m_id == id)
            {
                return node;
            }
        }

        return null;
    }

    //Recursivly finds the root quest node of any given node
    private Quest_Node GetRootQuest(Node n)
    {
        if (n.m_type == NodeType.Quest)
        {
            Debug.Log("There is a root Quest: " + n);
            return (Quest_Node)n;
        }
        List<Connection> connections = GetConnections(n.m_in_points[0]);
        foreach (Connection c in connections)
        {
            Debug.Log($"ConnectionInType: {c.m_in_point.m_type}   ConnectionOutType: {c.m_out_point.m_type}");
            return GetRootQuest(c.m_out_point.m_node);
        }

        return null;
    }

    private Step_Node GetStepNode(int id)
    {
        if (m_step_nodes is null) return null;
        foreach (var node in m_step_nodes)
        {
            if (node.m_id == id)
            {
                return node;
            }
        }
        return null;
    }

    // Adding new nods needs to cleaned quite badly
    private void OnClickAddQuestNode(Vector2 mousePosition)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_quest_nodes == null)
        {
            m_quest_nodes = new List<Quest_Node>();
        }
        if (m_quests_lists_data.m_quests == null)
        {
            m_quests_lists_data.m_quests = new List<QuestData>();
        }

        QuestData qd = new QuestData();
        qd.m_quest_name = "";
        qd.m_quest_description = "";
        qd.m_quest_reward = null;
        qd.m_quest_status = (int)Quest.QuestStatus.locked;
        qd.m_active_step_pos = 0;
        qd.m_steps = new List<StepData>();
        m_quests_lists_data.m_quests.Add(qd);

        Quest_Node n = new Quest_Node(mousePosition, 400, 150, m_next_node_id, m_node_style, m_selected_node_style, m_in_point_style, m_out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.Data = qd;
        m_nodes.Add(n);
        m_quest_nodes.Add(n);
        m_next_node_id++;
    }

    private void OnClickAddStepNode(Vector2 mousePosition)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_step_nodes == null)
        {
            m_step_nodes = new List<Step_Node>();
        }
        if (m_unassigned_steps == null)
        {
            m_unassigned_steps = new List<StepData>();
        }

        StepData sd = new StepData();
        sd.m_step_name = "";
        sd.m_step_description = "";
        m_unassigned_steps.Add(sd);

        Step_Node n = new Step_Node(mousePosition, 400, 100, m_next_node_id, m_node_style, m_selected_node_style, m_in_point_style, m_out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.m_data = sd;
        m_nodes.Add(n);
        m_step_nodes.Add(n);
        m_next_node_id++;
    }

    private void OnClickInPoint(ConnectionPoint inPoint)
    {
        m_selected_in_point = inPoint;

        if (m_selected_out_point != null)
        {
            if (m_selected_out_point.m_node != m_selected_in_point.m_node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickOutPoint(ConnectionPoint outPoint)
    {
        m_selected_out_point = outPoint;

        if (m_selected_in_point != null)
        {
            if (m_selected_out_point.m_node != m_selected_in_point.m_node)
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        RemoveStepData(connection);
        m_connections.Remove(connection);
    }

    private void OnClickRemoveNode(Node node)
    {
        if (m_connections != null)
        {
            List<Connection> connectionsToRemove = new List<Connection>();

            foreach (Connection c in m_connections)
            {
                if (node.m_in_points != null)
                {
                    // Do a check for every inpoint, not just one
                    foreach (ConnectionPoint p in node.m_in_points)
                    {
                        if (c.m_in_point == p)
                        {
                            connectionsToRemove.Add(c);
                        }
                    }
                }

                if (node.m_out_points != null)
                {
                    // Same with out points
                    foreach (ConnectionPoint p in node.m_out_points)
                    {
                        if (c.m_out_point == p)
                        {
                            connectionsToRemove.Add(c);
                        }
                    }
                }
            }

            foreach (Connection c in connectionsToRemove)
            {
                RemoveStepData(c);
                m_connections.Remove(c);
            }

            connectionsToRemove = null;
        }

        if (node.m_type == NodeType.Quest)
        {
            m_quests_lists_data.m_quests.Remove(((Quest_Node)node).Data);
        }

        m_nodes.Remove(node);
    }

    private void OnDrag(Vector2 delta)
    {
        m_drag = delta;

        if (m_nodes != null)
        {
            foreach (Node node in m_nodes)
            {
                node.Drag(delta);
            }
        }

        GUI.changed = true;
    }

    private void OnEnable()
    {
        m_quests_lists_data = new QuestsListData();

        m_node_style = new GUIStyle();
        m_node_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_node_style.border = new RectOffset(12, 12, 12, 12);

        m_selected_node_style = new GUIStyle();
        m_selected_node_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        m_selected_node_style.border = new RectOffset(12, 12, 12, 12);

        m_in_point_style = new GUIStyle();
        m_in_point_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        m_in_point_style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        m_in_point_style.border = new RectOffset(4, 4, 12, 12);

        m_out_point_style = new GUIStyle();
        m_out_point_style.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        m_out_point_style.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        m_out_point_style.border = new RectOffset(4, 4, 12, 12);
    }

    // Node based system from https://gram.gs/gramlog/creating-node-based-editor-unity/
    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);

        if (GUI.Button(new Rect(5, 5, 100, 50), "Save to JSON"))
        {
            SaveToJSON();
        }

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add Quest Node"), false, () => OnClickAddQuestNode(mousePosition));
        genericMenu.AddItem(new GUIContent("Add Step Node"), false, () => OnClickAddStepNode(mousePosition));

        genericMenu.ShowAsContext();
    }

    private void ProcessEvents(Event e)
    {
        m_drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (m_nodes != null)
        {
            // Start at end because last node is drawn at the top, so start there with updates
            for (int i = m_nodes.Count - 1; i >= 0; i--)
            {
                bool guiChanged = m_nodes[i].ProcessEvents(e);

                if (guiChanged)
                {
                    GUI.changed = true;
                }
            }
        }
    }

    //TODO: Removal needs to remove from data containers as well
    private void RemoveStepData(Connection connection)
    {
        Node disconnectedNode = connection.m_in_point.m_node;
        Quest_Node root = GetRootQuest(disconnectedNode);

        root.Data.m_steps.Remove(((Step_Node)disconnectedNode).m_data);

        m_unassigned_steps.Add(((Step_Node)disconnectedNode).m_data);
    }

    private void SaveToJSON()
    {
        JSONQuestIO.GetReader().SaveFile("quest_file", m_quests_lists_data);
    }
}