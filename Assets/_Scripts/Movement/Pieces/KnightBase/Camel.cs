using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camel : KnightBase
{

    void Start()
    {
        SqauresPerSide = new Vector2Int[] { new Vector2Int(3,1), new Vector2Int(1,3) };
        Lines = 3;
    }

    public override bool[,] GetMoves(Vector2Int _initPos) { return base.GetMoves(_initPos);}
}
