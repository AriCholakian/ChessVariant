using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Movement
{
    void Start()
    {
        MovementID = (int) PieceID.BISHOP;
        Lines = 3;
    }
    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int _occurence = 0;
        int _numCollisions = 0;
        var rst = new bool[14,14];
        int i = 1;
        Vector2Int target = new Vector2Int(0, 0);
        while (_occurence <= Lines ) 
        {
            target = new Vector2Int(_initPos.x, _initPos.y);
            i = 1;

            Vector2Int n = GetDirection(PieceSpecificDirection(_occurence));
            while (_numCollisions < 1 && InBounds(_initPos + (n * i)))
            {
                target = _initPos + (n * i);
                rst[target.x, target.y] = true;
                if (Manager.Instance.GetAtPosition(target) != null) _numCollisions++;

                i++;
            }
            _numCollisions = 0; 
            _occurence++;
        }
            
        return rst;
        
    }

    private int PieceSpecificDirection(int _occurence)
    {
        return _occurence * 2 + 1;
    }
}
