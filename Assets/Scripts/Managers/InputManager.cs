using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Dictionary<(Define.InputEvent, Define.InputType), Action<object[], uint>> Actions { get; set; } = new Dictionary<(Define.InputEvent, Define.InputType), Action<object[], uint>>();

    private bool _isPressed = false;
    private float _pressedTime = 0f;

    private Vector3 _dragStartPoint = Vector3.zero;

    public void Init()
    {
        for(int i = 0; i < (int)Define.InputEvent.END; i++)
        {
            for(int j = 0; j < (int)Define.InputType.END; j++)
            {
                Actions.Add(((Define.InputEvent)i, (Define.InputType)j), null);
            }
        }
    }

    public void OnUpdate()
    {
        if (Input.anyKey && Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)] != null)
            Actions[(Define.InputEvent.KeyEvent, Define.InputType.Down)].Invoke(null, 0);

        if (Actions[(Define.InputEvent.MouseEvent, Define.InputType.Down)]  != null ||
            Actions[(Define.InputEvent.MouseEvent, Define.InputType.Up)]    != null || 
            Actions[(Define.InputEvent.MouseEvent, Define.InputType.Press)] != null ||
            Actions[(Define.InputEvent.MouseEvent, Define.InputType.Drag)]  != null)
        {
            if (Input.GetMouseButton(0))
            {
                if (!_isPressed)
                {
                    Actions[(Define.InputEvent.MouseEvent, Define.InputType.Down)]?.Invoke(null, 0);
                    _pressedTime = Time.time;
                    _dragStartPoint = Input.mousePosition;
                }
                
                Actions[(Define.InputEvent.MouseEvent, Define.InputType.Press)]?.Invoke(null, 0);
                _isPressed = true;

                if (_dragStartPoint != Input.mousePosition)
                {
                    Vector3 dir = Input.mousePosition - _dragStartPoint;
                    Actions[(Define.InputEvent.MouseEvent, Define.InputType.Drag)]?.Invoke(new object[] { dir }, 1);
                }
            }
            else
            {
                if (_isPressed)
                {
                    Actions[(Define.InputEvent.MouseEvent, Define.InputType.Up)]?.Invoke(null, 0);
                }
                _isPressed = false;
                _pressedTime = 0f;
            }
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if(Actions[(Define.InputEvent.TouchEvent, Define.InputType.Down)] != null)
                    Actions[(Define.InputEvent.TouchEvent, Define.InputType.Down)]?.Invoke(null, 0);
            }
            else if (touch.phase == TouchPhase.Moved && touch.deltaPosition != Vector2.zero)
            {
                if (Actions[(Define.InputEvent.TouchEvent, Define.InputType.Drag)] != null)
                {
                    Actions[(Define.InputEvent.TouchEvent, Define.InputType.Drag)]?.Invoke(new object[] { touch.deltaPosition }, 1);
                }
                
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                if (Actions[(Define.InputEvent.TouchEvent, Define.InputType.Up)] != null)
                {
                    Actions[(Define.InputEvent.TouchEvent, Define.InputType.Up)]?.Invoke(null, 0);
                }
            }
        }
    }

    public void Clear()
    {
        foreach (var key in Actions.Keys.ToList())
        {
            Actions[key] = null;
        }
        Actions.Clear();
    }
}
