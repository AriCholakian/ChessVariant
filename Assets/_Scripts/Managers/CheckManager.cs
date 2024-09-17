using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
    [SerializeField] public List<KingPiece> KingList;  
    List<Movement> MovementOptions = new List<Movement>();
    protected Manager Mngr;  
    static Vector2Int[] directions = new Vector2Int[] { Vector2Int.right, Vector2Int.one, Vector2Int.up, new Vector2Int(-1, 1), Vector2Int.left, Vector2Int.one * -1, Vector2Int.down, new Vector2Int(1, -1)};

    
    void Awake()
    {
        Mngr = Manager.Instance;
    }
    void Start()
    {
        GetComponents<Movement>(results: MovementOptions);
    }

    public void UpdateCurrentChecks()
    {
        for (int i = 0; i < KingList.Count; i++)
        {
            KingList[i].isUnderCheck = CheckCheck(KingList[i].initPos, KingList[i]);
            SurroundingsUnderCheck(KingList[i].initPos, KingList[i]);
        }
    }
    public bool CausesCheck(Vector2Int _virtualPos, Side _activeSide)
    {
        return CheckCheck(_virtualPos, GetKingofSide(_activeSide));
    }
    public bool IsKingChecked(Side _activeSide)
    {
        return CheckCheck(GetKingofSide(_activeSide).initPos, GetKingofSide(_activeSide));
    }
    void SurroundingsUnderCheck(Vector2Int _targetPos, KingPiece _king)
    {
        for (int i = 0; i < directions.Length; i++)
        {
            //if (CheckCheck(_targetPos + directions[i], _king)) _king.SquaresUnderCheck.Add(_targetPos + directions[i]);
        }
    }

    public bool CheckCheck(Vector2Int _targetPos, KingPiece _king)
    {
        for (int i = 0; i < MovementOptions.Count; i++) 
        {   
            bool[,] _cast = MovementOptions[i].GetMoves(_targetPos);
            
            for (int x = 0; x < _cast.GetLength(0); x++)
            {
                for (int y = 0; y < _cast.GetLength(1); y++)
                {
                    if (_cast[x,y])
                    {
                        Piece _attacker = Mngr.GetAtPosition(new Vector2Int(x, y));

                        if (_attacker != null) 
                        {
                            if (_attacker.ID == MovementOptions[i].MovementID && _attacker.Side != _king.Side)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    public bool VirtualCheck(Piece[,] _virtualBoard, Side _activeSide)
    {
        KingPiece _activeKing = null;
        foreach (Piece king in Util.GetAllofID(1, _virtualBoard))
        {
            if (king.Side == _activeSide) _activeKing = (KingPiece) king;
        }
        for (int i = 0; i < MovementOptions.Count; i++) 
        {   
            bool[,] _cast = MovementOptions[i].GetMoves(_activeKing.initPos);
            
            for (int x = 0; x < _cast.GetLength(0); x++)
            {
                for (int y = 0; y < _cast.GetLength(1); y++)
                {
                    if (_cast[x,y])
                    {
                        Piece _attacker = Mngr.GetAtPosition(new Vector2Int(x, y));

                        if (_attacker != null) 
                        {
                            if (_attacker.ID == MovementOptions[i].MovementID && _attacker.Side != _activeKing.Side)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    KingPiece GetKingofSide(Side side)
    {
        foreach ( KingPiece king in KingList) 
        {
            if (king.Side == side) return king;
        } 
        throw new System.Exception("No King Found");
    }
    public void InitializeKingList()
    {
        List<KingPiece> o = new List<KingPiece>();
        foreach (Piece king in Util.GetAllofID(1, Mngr.Pieces))
        {
            o.Add((KingPiece) king);
        }
        KingList = o;
    }
}
