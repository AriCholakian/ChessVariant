using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Movement
{
    
    void Start()
    {
        MovementID = (int) PieceID.KING;
        Lines = 7;
    }
    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int _occurence = 0;
        var rst = new bool[14,14];
        while (_occurence <= Lines)
        {
            Vector2Int n = _initPos + GetDirection(_occurence);
            if (InBounds(n)) rst[n.x, n.y] = true;
            _occurence++;
        }
        return rst;
    }

}

