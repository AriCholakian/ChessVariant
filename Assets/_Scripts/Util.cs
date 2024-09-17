using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Util
{
    
    public static bool[,] Merge(bool[,] a, bool[,] b)
    {
        bool[,] c = new bool[14,14];
            for (int x = 0; x <= 13; x++)
            {
                for (int y = 0; y <= 13; y++)
                {
                    if (a[x,y] || b[x,y]) c[x,y] = true;
                }
            }
            return c;
    }

    public static void Reset(bool[,] a)
    {
        for (int x = 0; x <= 13; x++)
        {
            for (int y = 0; y <= 13; y++)
            {
               a[x,y] = false;
            }
        }
    }

    public static bool InBounds(Vector2Int targetPos)
    {
        if (targetPos.x <= 13 && targetPos.x >= 0 && targetPos.y <= 13 && targetPos.y >= 0) return true;
        else return false;
    }

    public static List<Piece> GetAllofID(int ID, Piece[,] pieces)
    {
        List<Piece> o = new List<Piece>();
        for (int x = 0; x < pieces.GetLength(0); x++)
        {
            for (int y = 0; y < pieces.GetLength(1); y++)
            {
                if (pieces[x,y]?.ID == ID) 
                {
                    o.Add(pieces[x,y]);
                }
            }
        }
        return o;
    }   

    public static Side ConvertTurnToSide(bool b) => b ? Side.WHITE : Side.BLACK;
    public static bool ConvertSideToTurn(Side side) => side == Side.WHITE ? true : false;

}
