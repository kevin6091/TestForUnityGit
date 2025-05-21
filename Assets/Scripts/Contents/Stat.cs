using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Stat : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _rotateSpeed;
    [SerializeField] protected int _maxStackSize;

    public int MaxStackSize { get { return _maxStackSize; } set { _maxStackSize = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float RotateSpeed { get { return _rotateSpeed; } set { _rotateSpeed = value; } }

    private void Awake()
    {
        //  Todo: Need Get World StatLevel
        MaxStackSize = Managers.Data.StatDict[1].maxStackSize;
        MoveSpeed = Managers.Data.StatDict[1].moveSpeed;
        RotateSpeed = Managers.Data.StatDict[1].rotateSpeed;

        //  Todo : Need Data Parse 
        //MaxStackSize = 4;
        //MoveSpeed = 5.0f;
        //RotateSpeed = 360f;
    }

    public void SyncNavMeshAgent(NavMeshAgent nma)
    {
        nma.speed = MoveSpeed;
        nma.angularSpeed = RotateSpeed;
    }
}
