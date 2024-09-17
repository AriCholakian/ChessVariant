using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : Piece
{
    public bool isUnderCheck;
    public List<Vector2Int> SquaresUnderCheck;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }
    protected override void CollectMoves()
    {
        Util.Reset(StoredMoves);
        for (int i = 0; i < MovementAbilities.Count; i++) 
        {
            StoredMoves = Util.Merge(StoredMoves, (bool[,]) (MovementAbilities[i].GetMoves(initPos).Clone()));
        }
        foreach (Vector2Int pos in SquaresUnderCheck)
        {
            StoredMoves[pos.x, pos.y] = false;
        }
        HasStoredMoves = true;
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

    public override void OnEndTurn()
    {
        base.OnEndTurn();
        SquaresUnderCheck.Clear();
    }

}
