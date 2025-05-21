using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<Define.State, State> _states = new Dictionary<Define.State, State>();
    private State _curState = null;
    private Define.State _curStateType = Define.State.END;

    static private List<String> s_names = new List<String> { "Player" };

    public void RegisterState<T>(Define.State stateType, MonoBehaviour context) where T : State
    {
        State state = Activator.CreateInstance(typeof(T), this, context) as State;
        _states[stateType] = state;
    }

    public Define.State CurStateType()
    {
        //  return CurState_FindName();
        return CurState_Caching();
    }

    public State CurState { get { return _curState; } }

    public Define.State CurState_FindName()
    {
        if (_curState == null ||
            _states.ContainsValue(_curState) == false)
            return Define.State.END;

        string name = _curState.GetType().Name;
        name = name.Substring("State".Length);
        string[] names = Enum.GetNames(typeof(Define.State));

        for (int i = 0; i < s_names.Count; ++i)
        {
            int idx = name.LastIndexOf(s_names[i]);
            if (idx != -1)
            {
                name = name.Substring(0, idx);
                break;
            }
        }

        for (int i = 0; i < names.Length; ++i)
        {
            if (name.Contains(names[i]))
                return (Define.State)i;
        }

        return Define.State.END;
    }

    public Define.State CurState_Caching()
    {
        return _curStateType;
    }

    public void ChangeState(Define.State stateType)
    {
        if (_states.ContainsKey(stateType) == false)
        {
            Debug.Log($"Change Undefined State {stateType.ToString()}");
            return;
        }

        if (_curState == _states[stateType])
        {
            return;
        }


        if (_curState != null)
        {
            _curState.Exit();
        }

        _curState = _states[stateType];
        _curStateType = stateType;

        if (_curState != null)
        {
            _curState.Enter();
        }
    }

    public void Execute()
    {
        if (_curState != null)
            _curState.Execute();
    }

}