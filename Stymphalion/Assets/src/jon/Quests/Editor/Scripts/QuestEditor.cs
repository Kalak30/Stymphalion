/*
 * Filename: QuestEditor.cs
 * Developer: Jon Kopf
 * Purpose: This is the window and controller for the QuetEditor tool.
 *          The tool allows for development of quests in a visual way and allow them to be saved to a JSON file
 *
 *          Node based system from https://gram.gs/gramlog/creating-node-based-editor-unity/
 */

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


/// <summary>
/// A window containing the node based system for editing quests
/// /// <list type="bullet">
///     <item>m_quests_lists_data</item>
///     <item>m_unassigned_steps</item>
///     <item>m_connections</item>
///     <item>m_drag</item>
///     <item>m_in_point_style</item>
///     <item>m_next_node_id</item>
///     <item>m_node_style</item>
///     <item>m_nodes</item>
///     <item>m_offset</item>
///     <item>m_out_point_style</item>
///     <item>m_quest_nodes</item>
///     <item>m_selected_in_point</item>
///     <item>m_selected_node_style</item>
///     <item>m_selected_out_point</item>
///     <item>m_step_nodes</item>
/// </list>
/// </summary>
public class QuestEditor : EditorWindow
{
    public QuestsListData m_quests_lists_data;
    public List<StepData> m_unassigned_steps;

    private List<Connection> m_connections;
    private Vector2 m_drag;
    private GUIStyle m_in_point_style;
    private int m_next_node_id;
    private GUIStyle m_node_style;
    private List<Node> m_nodes;
    private Vector2 m_offset;
    private GUIStyle m_out_point_style;
    private List<QuestNode> m_quest_nodes;
    private ConnectionPoint m_selected_in_point;
    private GUIStyle m_selected_node_style;
    private ConnectionPoint m_selected_out_point;
    private List<StepNode> m_step_nodes;

    /// <summary>
    /// Actions taken when opening this window. Located in Unity toolbar
    /// </summary>
    [MenuItem("QUESTS/Quest Editor")]
    public static void OpenWindow()
    {
        QuestEditor wnd = GetWindow<QuestEditor>();
        wnd.titleContent = new GUIContent("Quest Editor");
    }


    private QuestNode AddQuestNode(QuestData qd, Vector2 pos)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_quest_nodes == null)
        {
            m_quest_nodes = new List<QuestNode>();
        }



        QuestNode n = new QuestNode(pos, 400, 150, m_next_node_id, m_node_style, m_selected_node_style, m_in_point_style, m_out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.m_data = qd;
        m_nodes.Add(n);
        m_quest_nodes.Add(n);
        m_next_node_id++;

        return n;
    }

    private StepNode AddStepNode(StepData sd, Vector2 pos)
    {
        if (m_nodes == null)
        {
            m_nodes = new List<Node>();
        }
        if (m_step_nodes == null)
        {
            m_step_nodes = new List<StepNode>();
        }


        StepNode n = new StepNode(pos, 400, 100, m_next_node_id, m_node_style, m_selected_node_style, m_in_point_style, m_out_point_style, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode);
        n.m_data = sd;
        m_nodes.Add(n);
        m_step_nodes.Add(n);
        m_next_node_id++;

        return n;
    }

    /// <summary>
    /// Clears the current selected nodes
    /// </summary>
    private void ClearConnectionSelection()
    {
        m_selected_in_point = null;
        m_selected_out_point = null;
    }

    /// <summary>
    /// Creates a <see cref="Connection"/> between two <see cref="ConnectionPoint"/>
    /// </summary>
    private void CreateConnection(bool from_save_data)
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
                QuestNode qn = GetRootQuest(m_selected_in_point.m_node);
                if (qn != null && !from_save_data)
                {
                    qn.m_data.m_steps.Add(((StepNode)m_selected_in_point.m_node).m_data);
                    m_unassigned_steps.Remove(((StepNode)m_selected_in_point.m_node).m_data);
                }
            }
        }
        else
        {
            ClearConnectionSelection();
        }
    }

    /// <summary>
    /// Draws an active line between the first selected point and the current mouse position.
    /// This allows for the user to see that they are in fact creating a connection
    /// </summary>
    /// <param name="e"></param>
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

    /// <summary>
    /// Draw all <see cref="Connection"/> lines within the editor
    /// </summary>
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

    /// <summary>
    /// Draws a background grid. Gives the user a sense of movement when panning around
    /// </summary>
    /// <param name="grid_spacing"></param>
    /// <param name="grid_opacity"></param>
    /// <param name="grid_color"></param>
    private void DrawGrid(float grid_spacing, float grid_opacity, Color grid_color)
    {
        int widthDivs = Mathf.CeilToInt(position.width / grid_spacing);
        int heightDivs = Mathf.CeilToInt(position.height / grid_spacing);

        Handles.BeginGUI();
        Handles.color = new Color(grid_color.r, grid_color.g, grid_color.b, grid_opacity);

        m_offset += m_drag * 0.5f;
        Vector3 newOffset = new Vector3(m_offset.x % grid_spacing, m_offset.y % grid_spacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(grid_spacing * i, -grid_spacing, 0) + newOffset, new Vector3(grid_spacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-grid_spacing, grid_spacing * j, 0) + newOffset, new Vector3(position.width, grid_spacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    /// <summary>
    /// Draws all <see cref="Node"/> objects as windows.
    /// Different types of nodes are drawn differently
    /// </summary>
    private void DrawNodes()
    {
        BeginWindows();

        if (m_nodes != null)
        {
            foreach (Node n in m_nodes)
            {
                QuestNode questNode = GetQuestNode(n.m_id);
                StepNode stepNode = GetStepNode(n.m_id);

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

    /// <summary>
    /// Gets all <see cref="Connections"/> attached to a given <see cref="ConnectionPoint"/>
    /// </summary>
    /// <param name="p">Point to get connections from</param>
    /// <returns>
    /// List of connections attached to p
    /// </returns>
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

    /// <summary>
    /// Gets a quest node within the quest node list
    /// </summary>
    /// <param name="id">id value of node to find</param>
    /// <returns><see cref="Node"/> with id, or null if not in <see cref="m_quest_nodes"/></returns>
    private QuestNode GetQuestNode(int id)
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

    /// <summary>
    /// Recursivly finds the root quest node of any given node
    /// </summary>
    /// <param name="n">Node to find root quest from</param>
    /// <returns>Root quest node, or null if no root node exists</returns>
    private QuestNode GetRootQuest(Node n)
    {
        if (n.m_type == NodeType.Quest)
        {
            return (QuestNode)n;
        }
        List<Connection> connections = GetConnections(n.m_in_points[0]);
        foreach (Connection c in connections)
        {
            return GetRootQuest(c.m_out_point.m_node);
        }

        return null;
    }

    /// <summary>
    /// Gets a step node within the step node list
    /// </summary>
    /// <param name="id">id value of node to find</param>
    /// <returns><see cref="Node"/> with id, or null if not in <see cref="m_quest_nodes"/></returns>
    private StepNode GetStepNode(int id)
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

    /// <summary>
    /// Loads from the quest_file the current quest data
    /// </summary>
    private void LoadFromJSON()
    {

        if (m_unassigned_steps == null)
        {
            m_unassigned_steps = new List<StepData>();
        }
        QuestListBuilder builder = new QuestListBuilder();
        QuestListReader reader = new QuestListReader(builder);
        m_quests_lists_data = reader.ConstructData("quest_file");

        Vector2 placement_pos = new Vector2(10, 10);
        float padding = 50;

        foreach (QuestData qd in m_quests_lists_data.m_quests)
        {
            QuestNode qn = AddQuestNode(qd, placement_pos);
            Node previous_node = qn;

            foreach (StepData sd in qd.m_steps)
            {
                Vector2 step_pos = placement_pos + new Vector2(qn.m_rect.width + padding, 0);

                // Create Node
                m_unassigned_steps.Add(sd);
                StepNode sn = AddStepNode(sd, step_pos);


                // Create connection3
                m_selected_in_point = sn.m_in_points[0];
                m_selected_out_point = previous_node.m_out_points[0];
                CreateConnection(true);
                ClearConnectionSelection();
                previous_node = sn;

                // Move placement pos between steps
                placement_pos.y += sn.m_rect.height + padding / 10;
            }

            // Move placement pos between quests
            placement_pos.y += padding;
        }
    }

    /// <summary>
    /// Action to take when clicking on add a quest node.
    /// Creates a new node and a new data object to store its data.
    /// </summary>
    /// <param name="mouse_position"></param>
    private void OnClickAddQuestNode(Vector2 mouse_position)
    {

        if (m_quests_lists_data.m_quests == null)
        {
            m_quests_lists_data.m_quests = new List<QuestData>();
        }


        QuestData qd = new QuestData();
        qd.m_quest_name = "";
        qd.m_quest_description = "";
        qd.m_quest_reward = Item.ItemType.Gold;
        qd.m_quest_status = QuestStatus.locked;
        qd.m_active_step_pos = 0;
        qd.m_steps = new List<StepData>();
        m_quests_lists_data.m_quests.Add(qd);

        AddQuestNode(qd, mouse_position);
    }

    /// <summary>
    /// Action to take when clicking on add a step node
    /// Creates a new step node and a new data object to store it
    /// </summary>
    /// <param name="mouse_position"></param>
    private void OnClickAddStepNode(Vector2 mouse_position)
    {
        if (m_unassigned_steps == null)
        {
            m_unassigned_steps = new List<StepData>();
        }


        StepData sd = new StepData();
        sd.m_step_name = "";
        sd.m_step_description = "";
        m_unassigned_steps.Add(sd);

        AddStepNode(sd, mouse_position);
    }

    /// <summary>
    /// When a <see cref="ConnectionPoint"/> with <see cref="ConnectionPointType"/> == <see cref="ConnectionPointType.In"/>,
    /// a new connection should be created if there is both an in point and out point selected
    /// </summary>
    /// <param name="in_point">selected point</param>
    private void OnClickInPoint(ConnectionPoint in_point)
    {
        m_selected_in_point = in_point;

        if (m_selected_out_point != null)
        {
            if (m_selected_out_point.m_node != m_selected_in_point.m_node)
            {
                CreateConnection(false);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    /// <summary>
    /// When a <see cref="ConnectionPoint"/> with <see cref="ConnectionPointType"/> == <see cref="ConnectionPointType.Out"/>,
    /// a new connection should be created if there is both an in point and out point selected
    /// </summary>
    /// <param name="out_point">selected point</param>
    private void OnClickOutPoint(ConnectionPoint out_point)
    {
        m_selected_out_point = out_point;

        if (m_selected_in_point != null)
        {
            if (m_selected_out_point.m_node != m_selected_in_point.m_node)
            {
                CreateConnection(false);
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    /// <summary>
    /// Remove a connection when the remove connection button is pressed
    /// </summary>
    /// <param name="connection"></param>
    private void OnClickRemoveConnection(Connection connection)
    {
        RemoveStepData(connection);
        m_connections.Remove(connection);
    }

    /// <summary>
    /// Remove a node when the remove node button is pressed. Removes the all data associated with node from the data lists
    /// </summary>
    /// <param name="node"></param>
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
            m_quests_lists_data.m_quests.Remove(((QuestNode)node).m_data);
        }

        m_nodes.Remove(node);
    }

    /// <summary>
    /// Actions to take when the mouse is dragged on the editor window
    /// </summary>
    /// <param name="delta">Amount of space to move by</param>
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

    /// <summary>
    /// Initializes editor settings as well as Graphics styles
    /// </summary>
    private void OnEnable()
    {
        m_quests_lists_data = new QuestsListData();

        LoadFromJSON();

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

    /// <summary>
    /// Automatically gets called. Draws the editor window
    /// </summary>
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

    /// <summary>
    /// Gives the user a context menu so they have some form of options when editing.
    /// </summary>
    /// <param name="mouse_position"></param>
    private void ProcessContextMenu(Vector2 mouse_position)
    {
        GenericMenu generic_menu = new GenericMenu();
        generic_menu.AddItem(new GUIContent("Add Quest Node"), false, () => OnClickAddQuestNode(mouse_position));
        generic_menu.AddItem(new GUIContent("Add Step Node"), false, () => OnClickAddStepNode(mouse_position));

        generic_menu.ShowAsContext();
    }

    /// <summary>
    /// Process all incoming events happening on the editor window
    /// </summary>
    /// <param name="e"></param>
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

    /// <summary>
    /// Process all events happening to nodes within the editor
    /// </summary>
    /// <param name="e"></param>
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

    /// <summary>
    /// Removes the step data from quest data list
    /// </summary>
    /// <param name="connection"></param>
    private void RemoveStepData(Connection connection)
    {
        Node disconnectedNode = connection.m_in_point.m_node;
        QuestNode root = GetRootQuest(disconnectedNode);

        root.m_data.m_steps.Remove(((StepNode)disconnectedNode).m_data);

        m_unassigned_steps.Add(((StepNode)disconnectedNode).m_data);
    }

    /// <summary>
    /// Saves the quests within the editor to a SJON file named 'quest_file' in the persistent data path
    /// </summary>
    private void SaveToJSON()
    {
        QuestListWriter writer = new QuestListWriter();
        writer.SaveFile("quest_file", m_quests_lists_data);
    }
}