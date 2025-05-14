using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequest
{
    public PathRequest(Vector2Int start, Vector2Int goal, Vector2Int size, Action<List<Vector2Int>> onComplete)
    {
        Start = start;
        Goal = goal; 
        Size = size;
        OnComplete = onComplete;
    }

    //  Default Param
    private Vector2Int _start;
    private Vector2Int _goal;
    private Vector2Int _size;
    public Action<List<Vector2Int>> _onComplete;

    public Vector2Int Start { get { return _start; } private set { _start = value; } }
    public Vector2Int Goal { get { return _goal; } private set { _goal = value; } }
    public Vector2Int Size { get { return _size; } private set { _size = value; } }
    public Action<List<Vector2Int>> OnComplete { get { return _onComplete; } private set { _onComplete = value; } }
}
