using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Side
{
    WHITE, BLACK, UNSPECIFIED    
}
public class Piece : MonoBehaviour
{


    [SerializeField] Collider2D clider;
    public Vector2Int initPos, newPos;
    public Vector2 currPos;
    public List<Movement> MovementAbilities = new List<Movement>();
    protected Manager Mngr;
    protected bool HasStoredMoves, IsSelected;
    public bool[,] StoredMoves;
    [SerializeField] public Side Side;
    [SerializeField] public PieceID PieceID;
    int id;
    public int ID
    {
        get { if (id != 0) { return id; } else { throw new System.Exception("Piece doesn't have type"); } }
        set { if (value != 0 ) { id = value; } else {throw new System.Exception("ID is trying to be set as UNSPECIFIED");}}
    }


    protected virtual void Awake()
    {
        id = (int) PieceID;
    }

    protected virtual void Start()
    {


        currPos = new Vector2(transform.position.x, transform.position.y);
        initPos = Vector2Int.RoundToInt(currPos);
        
        GetComponents<Movement>(results: MovementAbilities);
        StoredMoves = new bool[14,14];
        HasStoredMoves = false;

        Mngr = Manager.Instance;
        Mngr.SetIntoPosition(initPos, this);

        Mngr.Ended += OnEndTurn;
        Mngr.PieceDeselected += OnDeselect;

    }



    protected virtual void OnMouseDown()
    {
        clider.enabled = false;
        initPos = Vector2Int.RoundToInt(currPos);
        Mngr.RemoveFromPosition(initPos);


        if (!HasStoredMoves) 
        {
            CollectMoves();
        }

        IsSelected = !IsSelected;
        if (IsSelected)
        {
            Mngr.Select(this);
        }
        else Mngr.Deselect();

    }
    protected virtual void OnMouseDrag()
    {
        transform.position = new Vector3(Input.mousePosition.x / 77.142385f - 0.5f, Input.mousePosition.y / 77.142385f - 0.5f, -3);
    }
    protected virtual void OnMouseUp()
    {

        clider.enabled = true;
        newPos = FindNewPos();
        
        Mngr.Move(newPos);

    }

    protected virtual void CollectMoves()
    {
        Util.Reset(StoredMoves);
        for (int i = 0; i < MovementAbilities.Count; i++) 
        {
            StoredMoves = Util.Merge(StoredMoves, (bool[,]) (MovementAbilities[i].GetMoves(initPos).Clone()));
        }
        // RemoveIllegalMoves();
        HasStoredMoves = true;
    }

    // protected virtual void RemoveIllegalMoves()
    // {
    //     for (int x = 0; x <= 13; x++)
    //     {
    //         for (int y = 0; y <= 13; y++)
    //         {
    //             Piece target = Mngr.GetAtPosition(new Vector2Int(x,y));
    //             if (target?.Side == this.Side) StoredMoves[x,y] = false;
    //         }
    //     }
    // }

    protected Vector2Int FindNewPos()
    {
        return Vector2Int.RoundToInt(transform.position); 
    }

    public void MoveTo(Vector2Int _pos)
    {
        transform.position = new Vector3(_pos.x, _pos.y, -2);
    }


    public virtual void OnEndTurn()
    {
        HasStoredMoves = false;
    }
    public void OnSelect()
    {
        
    }
    public void OnDeselect()
    {
        IsSelected = false;
    }

    public void Disable()
    {
        Mngr.RemoveFromPosition(Vector2Int.RoundToInt(currPos));
        this.gameObject.SetActive(false);
    }
    





}

public enum PieceID {
    UNSPECIFIED,
    KING,
    PAWN,
    PRINCE,
    LION,
    KNIGHT,
    CAMEL,
    GIRAFFE,
    BUFFALO,
    BISHOP,
    MISSIONARY,
    ARCHBISHOP,
    CROCODILE,
    RHINO,
    ELEPHANT,
    ROOK,
    ADMIRAL,
    ARCHROOK,
    CANNON,
    BALLISTA,
    MACHINE,
    QUEEN,
    ARCHQUEEN,
    EMPRESS,
    DUCHESS
}

