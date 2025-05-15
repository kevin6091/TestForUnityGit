using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    Dictionary[Tuple<>]
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseActions = null;
    public List<Action> TouchActions = new List<Action>();

    bool _isPressed = false;
    float _pressedTime = 0f;
    public void OnUpdate()
    {
        if(Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
        
        if(MouseActions != null)
        {
            if (Input.GetMouseButton(0))
            {
                if(!_isPressed)
                {
                    MouseActions.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time;
                }
                MouseActions.Invoke(Define.MouseEvent.Press);
                _isPressed = true;
            }
            else
            {
                if (_isPressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                        MouseActions.Invoke(Define.MouseEvent.Click);
                    MouseActions.Invoke(Define.MouseEvent.PointerUp);
                }
                _isPressed = false;
                _pressedTime  = 0f;
            }
        }

        if(TouchActions[(int)Define.TouchEvent.Begin] != null)
        {
            if (GetHashCode)
            {
                
            }
            TouchActions[(int)Define.TouchEvent.Begin].Invoke();
        }
        
        //Touch touch = Input.GetTouch(0);
        //touch.
    }

    public void Clear()
    {
        KeyAction = null;
        MouseActions = null;
    }
}
