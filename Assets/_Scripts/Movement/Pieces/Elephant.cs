using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elephant : Movement
{
    void Start()
    {
        Lines = 3;
    }
    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int _occurence = 0;
        var rst = new bool[14,14];
        for (int i = 1; i <= 2; i++)
        {
            _occurence = 0;
            while (_occurence <= Lines)
            {
                Vector2Int n = _initPos + (GetDirection(PieceSpecificDirection(_occurence)) * i);
                
                if (InBounds(n)) rst[n.x, n.y] = true;
                _occurence++;
            }
        }
        return rst;
    }

    protected int PieceSpecificDirection(int _occurence)
    {
        return _occurence * 2 + 1;
    }
}
