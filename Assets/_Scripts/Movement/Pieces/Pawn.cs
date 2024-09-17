using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Movement
{
    private Vector2Int[] direction;
    void Start()
    {
        MovementID = (int) PieceID.PAWN;
        direction =  new Vector2Int[] { GetDirection(MoveType.NORTHEAST), GetDirection(MoveType.NORTHWEST), GetDirection(MoveType.SOUTHWEST), GetDirection(MoveType.SOUTHEAST), GetDirection(MoveType.NORTH), GetDirection(MoveType.SOUTH) };
    }
    
    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int i = 0;
        var rst = new bool[14,14];

        Vector2Int _target;

        while (i < 4) 
        {
            _target = direction[i] + _initPos;
            if (Manager.Instance.CheckForPiece(_target)) rst[_target.x, _target.y] = true;
            i++;
        }   

        while(i < 6)
        {
            int n = 1;
            while(n <= 2)
            {
                _target = _initPos + (direction[i] * n);
                if(Manager.Instance.CheckForPiece(_target)) break;
                rst[_target.x, _target.y] = true;
                n++;
            }
            i++;
        }

        return rst;
    }

}
