using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Tile
{
    /* Const Static */
    public const int        EMPTY = -1;

    /* CTor */
    public Tile(int typeID = -1, GameObject gameObject = null)
    {
        _typeID = typeID;
        _object = gameObject;
    }

    public int              _typeID;
    public GameObject       _object;
}


public class TileSystem : MonoBehaviour
{
    private List<List<Tile>>        _map = new List<List<Tile>>();

    void Awake()    //  ��ü ��������
    {
    }

    void Start()    //  ù ������
    {
        
    }

    void OnEnable() //  Active�ø��� 
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void Update()   // Update is called once per frame
    {
        
    }

    void LateUpdate()
    {
        
    }
}
