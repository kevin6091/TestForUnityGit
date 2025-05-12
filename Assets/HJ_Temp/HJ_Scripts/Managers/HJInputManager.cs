using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HJInputManager
{
    private Action KeyAction = null;

	void OnUpdate()
    {
        if (Input.anyKey == false)
            return;

        if (KeyAction != null)
            KeyAction.Invoke();
    }
}
