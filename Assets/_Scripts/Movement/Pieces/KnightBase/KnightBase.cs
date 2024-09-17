using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KnightBase : Movement
{
    protected Vector2Int[] SqauresPerSide;
    void Start()
    {
        Lines = 3;

    }
    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int _occurence = 0;
        var rst = new bool[14,14];

        Vector2Int val;

        while (_occurence <= Lines) 
        {
            Vector2Int n = GetDirection(PieceSpecificDirection(_occurence));
            
            for (int i = 0; i < SqauresPerSide.Length; i++)
            {
                val = _initPos + (n * SqauresPerSide[i]);
                if (InBounds(val)) rst[val.x, val.y] = true;
            }
            _occurence++;

        }
        return rst;
    }

    protected int PieceSpecificDirection(int _occurence)
    {
        return _occurence * 2 + 1;
    }
}
