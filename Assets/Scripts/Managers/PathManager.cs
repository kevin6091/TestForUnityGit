using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PathManager
{
    private readonly Queue<Action> _mainThreadQueue = new Queue<Action>();

    public void OnUpdate()
    {
        lock (_mainThreadQueue)
        {
            while (_mainThreadQueue.Count > 0)
            {
                _mainThreadQueue.Dequeue().Invoke();
            }
        }
    }

    public void MakePath(Vector2Int start, Vector2Int goal, Action<List<Vector2Int>> onComplete)
    {
        PathRequest request = new PathRequest(start, goal, Managers.Tile.Size, onComplete);

        ThreadPool.QueueUserWorkItem(_ =>
        {
            List<Vector2Int> path = PathFinder.MakePath(request);

            lock (_mainThreadQueue)
                _mainThreadQueue.Enqueue(() => request.OnComplete?.Invoke(path));
        });
    }
}
