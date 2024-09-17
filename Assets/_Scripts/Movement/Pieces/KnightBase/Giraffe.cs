using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giraffe : KnightBase
{

    void Start()
    {
        SqauresPerSide = new Vector2Int[] { new Vector2Int(3,2), new Vector2Int(2,3) };
        Lines = 3;
    }

    public override bool[,] GetMoves(Vector2Int _initPos) { return base.GetMoves(_initPos);}
}
