using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected uint _maxStackSize;

    public uint MaxStackSize { get { return _maxStackSize; } set { _maxStackSize = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float RotateSpeed { get { return _rotateSpeed; } set { _rotateSpeed = value; } }

    private void Start()
    {
        //  Todo : Need Data Parse 
        MaxStackSize = 4;
        MoveSpeed = 5.0f;
        RotateSpeed = 500.0f;
    }
}
