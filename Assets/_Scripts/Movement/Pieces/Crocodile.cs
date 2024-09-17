using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crocodile : Movement
{
    void Start()
    {
        Lines = 3;
    }

    public override bool[,] GetMoves(Vector2Int _initPos)
    {
        int _occurence = 0;
        int _numCollisions = 0;
        var rst = new bool[14,14];
        Vector2Int _target = new Vector2Int(0, 0);
        while (_occurence <= Lines ) 
        {
            _target = new Vector2Int(_initPos.x, _initPos.y);
            int i = 1;

            Vector2Int n = GetDirection(PieceSpecificDirection(_occurence));
            
            
            while (_numCollisions == 0 && InBounds(_initPos + (n * i)))
            {   
                
                _target = _initPos + (n * i);
                if (Manager.Instance.CheckForPiece(_target)) {
                    i++;
                    _numCollisions++;
                    break;
                }
                rst[_target.x, _target.y] = true;
                i++;
            }
            while (_numCollisions == 1 && InBounds(_initPos + (n * i)))
            {
                _target = _initPos + (n * i);
                if (Manager.Instance.CheckForPiece(_target)) {
                    rst[_target.x, _target.y] = true;
                    _numCollisions++;
                    break;
                }
                i++;
            }

            _occurence++;
            _numCollisions = 0; 
        }
            
        return rst;
        
    }

    private int PieceSpecificDirection(int _occurence)
    {
        return _occurence * 2 + 1;
    }


}
