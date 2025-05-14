using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Node : IComparable<Node>
{
    public Node(Vector2Int pos, float g_Cost = 0f, float h_cost = 0f)
    {
        Pos = pos;
        G_Cost = g_Cost;
        H_Cost = h_cost;
    }

    private float _g_Cost = 0f;
    private float _h_Cost = 0f;
    private Vector2Int _pos;

    public float G_Cost { get { return _g_Cost; } set { _g_Cost = value; } }
    public float H_Cost { get { return _h_Cost; } set { _h_Cost = value; } }
    public float F_Cost { get { return H_Cost + G_Cost; } }
    public Vector2Int Pos { get { return _pos; } set { _pos = value; } }
    public int CompareTo(Node other)
    {
        if (other == null)
            return 1;

        return F_Cost.CompareTo(other.F_Cost);
    }
}