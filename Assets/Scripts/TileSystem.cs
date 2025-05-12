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

    void Awake()    //  객체 생성시점
    {
    }

    void Start()    //  첫 프레임
    {
        
    }

    void OnEnable() //  Active시마다 
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
