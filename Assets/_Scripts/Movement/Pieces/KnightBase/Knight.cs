using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : KnightBase
{

    void Start()
    {
        SqauresPerSide = new Vector2Int[] { new Vector2Int(2,1), new Vector2Int(1,2) };
        Lines = 3;
    }

    public override bool[,] GetMoves(Vector2Int _initPos) { return base.GetMoves(_initPos);}

}
