using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Step_Node : Node
{
    public Step_Node(Vector2 position, float width, float height, int ID, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint, Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode) : base(position, width, height, ID, nodeStyle, selectedStyle, inPointStyle, outPointStyle, OnClickInPoint, OnClickOutPoint, OnClickRemoveNode)
    {
    }
}