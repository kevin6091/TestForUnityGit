using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    Texture2D _attackCursorIcon = null;
    Texture2D _handCursorIcon = null;
    CursorType _cursorType = CursorType.None;

    public enum CursorType
    {
        None,
        Attack,
        Hand,
    }

    void Start()
    {
        _attackCursorIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handCursorIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

	void Update()
    {
        if (Input.GetMouseButton(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float dist = 100f;

        RaycastHit hit;
        int layerMask = (1 << (int)Define.Layer.Floor) | (1 << (int)Define.Layer.Monster);

        if (Physics.Raycast(ray, out hit, dist, layerMask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType != CursorType.Attack)
                {
                    Cursor.SetCursor(_attackCursorIcon, new Vector2(_attackCursorIcon.width / 4, 0), CursorMode.Auto);
                    _cursorType = CursorType.Attack;
                }
            }

            else
            {
                if (_cursorType != CursorType.Hand)
                {
                    Cursor.SetCursor(_handCursorIcon, new Vector2(_handCursorIcon.width / 5, 0), CursorMode.Auto);
                    _cursorType = CursorType.Hand;
                }
            }
        }
    }
}
