using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : Piece
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
    }

    protected override void CollectMoves()
    {
        Util.Reset(StoredMoves);
        for (int i = 0; i < MovementAbilities.Count; i++) 
        {
            bool[,] rst = (bool[,]) (MovementAbilities[i].GetMoves(initPos).Clone());
            int n = PawnDirect(Side);
            for (int x = initPos.x - 1; x <= initPos.x + 1; x++)
            {
                int y = initPos.y;
                int j = 0;
                while (j < 3)
                {
                    if (Util.InBounds(new Vector2Int(x,y)))
                    {
                        rst[x,y] = false;
                    }
                    y -= n;
                    j++;
                }
            }
            StoredMoves = Util.Merge(StoredMoves, rst);
        }
        HasStoredMoves = true;
    }

    int PawnDirect(Side side)
    {
        switch (side)
        {
            case Side.WHITE: return 1;
            case Side.BLACK: return -1;
            default: throw new System.Exception("Piece Doesn't hava a Side Colour");
        }
    }
}
