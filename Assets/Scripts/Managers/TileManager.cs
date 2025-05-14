using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager
{
    //  Contain
    private List<List<Tile>>        _tiles = new List<List<Tile>>();

    //  Infos
    private Vector2Int  _size;
    private float       _offset = 1f;

    //  Property
    public      float Offset { get { return _offset; } set { _offset = value; } }
    public      Vector2Int Size { get { return _size; } private set { _size = value; } }

    private bool IsOutOfRange(Vector2Int pos)
    {
        if (pos.x >= Size.x ||
            pos.y >= Size.y)
            return true;

        return false;
    }

    public Tile GetTile(Vector2Int pos)
    {
        if (IsOutOfRange(pos))
            return null;

        return _tiles[pos.y][pos.x]; 
    }

    public void SetTile(Vector2Int pos, Tile tile)
    {
        if (IsOutOfRange(pos))
            return;

        _tiles[pos.y][pos.x] = tile;
    }

    public void Resize(Vector2Int size)
    {
        if (Size.y < size.y)
        {
            for (int i = Size.y; i < size.y; ++i)
                _tiles.Add(new List<Tile>());
        }
        else if(Size.y > size.y)
        {
            _tiles.RemoveRange(size.y, Size.y);

        }

        if (Size.x < size.x)
        {
            for(int y = 0; y < size.y; ++y)
            {
                for (int i = Size.x; i < size.x; ++i)
                    _tiles[y].Add(null);
            }
        }

        else if (Size.x > size.x)
        {
            for (int y = 0; y < size.y; ++y)
                _tiles.RemoveRange(size.x, Size.x);
        }

        Size = size;
    }

    public void Init(Vector2Int size, float offset = 1f)
    {
        Resize(size);
        Offset = offset;

        //  Test

        for(int y = 0; y < size.y; ++y)
        {
            for(int x = 0; x < size.x; ++x)
            {
                int rand = Random.Range(1, 10);
                if (rand <= 1)
                {
                    _tiles[y][x] = null;
                }
                else
                {
                    _tiles[y][x] = new Tile();
                }
            }
        }

    }
}
