using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class PathFinder
{
    //  Buffers
    static private List<Vector2Int> _routeBuffer = new List<Vector2Int>();
    static private HashSet<Vector2Int> _openList = new HashSet<Vector2Int>();
    static private HashSet<Vector2Int> _closeList = new HashSet<Vector2Int>();
    static private Dictionary<Vector2Int, Vector2Int> _parentMap = new Dictionary<Vector2Int, Vector2Int>();
    static private Dictionary<Vector2Int, float> _g_CostMap = new Dictionary<Vector2Int, float>();

    static private PathRequest _request = null;

    //  Supports Readonly
    private static readonly Vector2Int[] _dirs = new Vector2Int[] {
        new Vector2Int(1, 0), new Vector2Int(-1, 0),
        new Vector2Int(0, 1), new Vector2Int(0, -1)
    };

    static private void Open(Vector2Int pos) { _openList.Add(pos); }
    static private void Close(Vector2Int pos) { _closeList.Add(pos); }
    static private bool IsOpen(Vector2Int pos) { return _openList.Contains(pos); }
    static private bool IsClose(Vector2Int pos) { return _closeList.Contains(pos); }
    static private bool IsGoal(Node node) { return node.Pos == _request.Goal; }
    static private bool IsStart(Vector2Int pos) { return pos == _request.Start; }

    static public List<Vector2Int> MakePath(PathRequest request)
    {
        if (null == request)
            return new List<Vector2Int>();

        _request = request;
        ClearBuffers();

        Stopwatch stopwatch = Stopwatch.StartNew();

        if (!Initiate())
            UnityEngine.Debug.Log("Failed To Make Path");

        stopwatch.Stop();
        UnityEngine.Debug.Log("Compute Time : " + stopwatch.ElapsedMilliseconds);

        return _routeBuffer;
    }

    static private void ClearBuffers()
    {
        _routeBuffer.Clear();
        _openList.Clear();
        _closeList.Clear();
        _parentMap.Clear();
        _g_CostMap.Clear();
    }

    static private List<Vector2Int> GetNeighbors(Vector2Int pos)
    {
        List<Vector2Int> neighbors = new List<Vector2Int>();

        foreach (var dir in _dirs)
        {
            Vector2Int next = pos + dir;
            if (0 > next.x || next.x >= _request.Size.x ||
                0 > next.y || next.y >= _request.Size.y)
                continue;

            if (null == Managers.Tile.GetTile(next))
                continue;

            neighbors.Add(next);
        }

        return neighbors;
    }

    static private float ComputeGCost(Vector2Int next, Node cur)
    {
        return (next - cur.Pos).magnitude + cur.G_Cost;
    }

    static private float ComputeHCost(Vector2Int pos)
    {
        float result = 0;

        result = (_request.Goal - pos).magnitude;

        return result;
    }

    static private bool Initiate()
    {
        MinHeap<Node> pq = new MinHeap<Node>();
        pq.Add(new Node(_request.Start, 0f, ComputeHCost(_request.Start)));

        while (0 < pq.Count)
        {
            Node cur = pq.Pop();

            if (IsGoal(cur))
            {
                BuildRoute();
                return true;
            }

            Close(cur.Pos); 

            foreach (Vector2Int neighborPos in GetNeighbors(cur.Pos))
            {
                if (IsClose(neighborPos))
                    continue;

                float g_Cost = ComputeGCost(neighborPos, cur);
                float h_Cost = ComputeHCost(neighborPos);
                float f_Cost = g_Cost + h_Cost;

                if (_g_CostMap.ContainsKey(neighborPos) &&
                    _g_CostMap[neighborPos] <= g_Cost)
                    continue;

                _g_CostMap[neighborPos] = g_Cost;
                _parentMap[neighborPos] = cur.Pos;
                Open(neighborPos);

                pq.Add(new Node(neighborPos, g_Cost, h_Cost));
            }
        }

        return false;
    }

    static private void BuildRoute()
    {
        _routeBuffer.Add(_request.Goal);

        Vector2Int cur = _request.Goal;
        Vector2Int parent = _parentMap[cur];

        while(true)
        {
            cur = parent;
            _routeBuffer.Add(cur);
            if (IsStart(cur))
                break;

            parent = _parentMap[cur];
        }

        _routeBuffer.Reverse();
    }
}
