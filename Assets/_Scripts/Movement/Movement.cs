using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MoveType
{
    EAST = 0, NORTHEAST = 1, NORTH = 2, NORTHWEST = 3, WEST = 4, SOUTHWEST = 5, SOUTH = 6, SOUTHEAST = 7
}


public abstract class Movement : MonoBehaviour
{
    protected int Lines;
    public abstract bool[,] GetMoves(Vector2Int _initPos);
    public int MovementID;

    protected Vector2Int GetDirection(MoveType type) 
    {
        switch (type)
        {
            case MoveType.EAST: return Vector2Int.right;
            case MoveType.NORTHEAST: return Vector2Int.one;
            case MoveType.NORTH: return Vector2Int.up;
            case MoveType.NORTHWEST: return new Vector2Int(-1, 1);
            case MoveType.WEST: return Vector2Int.left;
            case MoveType.SOUTHWEST: return Vector2Int.one * -1;
            case MoveType.SOUTH: return Vector2Int.down;
            case MoveType.SOUTHEAST: return new Vector2Int(1, -1);
            default: return Vector2Int.zero;
        }

    }

    protected Vector2Int GetDirection(int type) 
    {
        switch (type)
        {
            case 0: return Vector2Int.right;
            case 1: return Vector2Int.one;
            case 2: return Vector2Int.up;
            case 3: return new Vector2Int(-1, 1);
            case 4: return Vector2Int.left;
            case 5: return Vector2Int.one * -1;
            case 6: return Vector2Int.down;
            case 7: return new Vector2Int(1, -1);
            default: return Vector2Int.zero;
        }

    } 
    public bool InBounds(Vector2Int targetPos)
    {
        if (targetPos.x <= 13 && targetPos.x >= 0 && targetPos.y <= 13 && targetPos.y >= 0) return true;
        else return false;
    }

}
