using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Define
{
    public enum State
    {
        Die,
        Move,
        Idle,
        Work,
        Wait,
        Corrupt,
        END,
    }

    public enum ItemType
    {
        Pizza,
        Trash,
        END,
    }

    public enum ProbType
    {
        Stand,
        Table,
        END,
    }

    public enum CreatureType
    {
        Employee,
        Player,
        Customer,
        END,
    }

    public enum Sequence
    {
        END,
    }

    public enum Layer
    {
        Wall = 8,
        Floor,
        Block,
        Monster,
    }

    public enum Scene
    {
        Unknown,    //  Default
        Login,      //  Main
        Lobby,
        Game,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Down,
        Up,
    }

    public enum InputEvent
    {
        KeyEvent,
        MouseEvent,
        TouchEvent,
        END,
    }

    public enum InputType
    { 
        Down,
        Press,
        Up,
        Drag,
        END,
    }

    public enum CameraMode
    {
        QuarterView,
        TopView,
        Free,
    }

    public enum WorkType
    {
        Buger,
        BugerServing,
        Trash,
        Counter,
    }

    public enum Worker
    {
        Player,
        Employee,
        None,
    }
}

public interface IKeyHandler
{
    public void OnKeyboard();
}