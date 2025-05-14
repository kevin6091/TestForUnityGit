using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TilemapAgent : MonoBehaviour
{
    List<Vector2Int> route = new List<Vector2Int>();
    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = transform.gameObject.AddComponent<LineRenderer>();
    }

	void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector2Int start = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            Vector2Int goal = new Vector2Int((int)Managers.Tile.Size.x - 1, (int)Managers.Tile.Size.y - 1);
            Managers.Path.MakePath(start, goal, OnPathFound);
        }

        if (0 < route.Count)
        {
            for (int i = 1; i < route.Count; ++i)
            {
                Debug.DrawLine(new Vector3(route[i - 1].x, 0f, route[i - 1].y), new Vector3(route[i].x, 0f, route[i].y));
            }

        }
    }
    void OnPathFound(List<Vector2Int> path)
    {
        // 여기서는 메인 스레드이므로 Unity 오브젝트 접근 OK
        Debug.Log("Path received! Length: " + path.Count);
        route = new List<Vector2Int>(path);
    }
}
