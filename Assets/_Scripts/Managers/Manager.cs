using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Notify();
public class Manager : MonoBehaviour
{
    [SerializeField] private GridManager Grid;
    [SerializeField] private CheckManager checkManager;
    public static Manager Instance { get; private set; }
    private Piece[,] pieces;
    public Piece[,] Pieces
    {
        get { return pieces; }
        private set { pieces = value; }
    }
    Piece SelectedPiece;
    public bool IsWhiteTurn
    {
        get;
        private set;
    }
    bool isWhiteTurn;
        
    public event Notify Ended, PieceDeselected;
    private void Awake() 
    { 
        
        if (Instance != null && Instance != this) 
        {
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 

        pieces = new Piece[14,14];
        Pieces = pieces;

        IsWhiteTurn = true;

    }

    void Start()
    {
        checkManager.InitializeKingList();
    }

    public Piece GetAtPosition(Vector2Int _pos)
    {
        return Pieces[_pos.x, _pos.y];
    }

    public void SetIntoPosition(Vector2Int _pos, Piece _piece)
    {
        Pieces[_pos.x, _pos.y] = _piece;
    }

    public void RemoveFromPosition(Vector2Int _pos)
    {
        Pieces[_pos.x, _pos.y] = null;
    }

    public bool CheckForPiece(Vector2Int _pos)
    {
        if (Util.InBounds(new Vector2Int(_pos.x, _pos.y)))
        {
            if (Pieces[_pos.x, _pos.y] != null) return true;
        }
        return false;
    }

    // public void TryMove(Vector2Int _newPos)
    // {
    //     Vector2Int _initPos = SelectedPiece.initPos;
    //     bool[,] _moves = SelectedPiece.StoredMoves;

    //     if (Util.InBounds(_newPos) && _moves[_newPos.x, _newPos.y])
    //     {   
    //         if (GetAtPosition(_newPos)?.Side == SelectedPiece.Side) {FailMove(_initPos); }
    //         else 
    //         {
    //             if (GetAtPosition(_newPos)?.Side != SelectedPiece.Side) 
    //             {
    //                 GetAtPosition(_newPos)?.Disable(); // take!
    //             }
    //             SetIntoPosition(_newPos, SelectedPiece);
    //             SelectedPiece.currPos = _newPos;
    //             SelectedPiece.MoveTo(_newPos);
                
    //             EndTurn();
    //             Deselect();
    //         }
    //     } 
    //     else
    //     {
    //         FailMove(_initPos);
    //     }
        
    // }

    // void FailMove(Vector2Int _inpo)
    // {
    //     SetIntoPosition(_inpo, SelectedPiece);
    //     SelectedPiece.MoveTo(_inpo);
    // }

    public Piece[,] CreateVirtualBoard(Vector2Int _newPos, Vector2Int _initPos, Piece _selectedPiece)
    {
        Piece[,] _virtualBoard = new Piece[13,13];
        Piece tempholder = null;
        
        if (GetAtPosition(_newPos) != null) tempholder = GetAtPosition(_newPos);
        
        SetIntoPosition(_newPos, _selectedPiece);
        
        _virtualBoard = (Piece[,]) pieces.Clone();
        
        RemoveFromPosition(_newPos);
        SetIntoPosition(_initPos, _selectedPiece);
        
        if (tempholder != null) SetIntoPosition(_newPos, tempholder);

        return _virtualBoard;
    }

    public void Move(Vector2Int _newPos)
    {
        Vector2Int _initPos = SelectedPiece.initPos;
        Piece piece = SelectedPiece;
        AttemptMove(_newPos, piece);

    }

    private bool AttemptMove(Vector2Int _newPos, Piece piece)
    {
        if (Util.ConvertTurnToSide(IsWhiteTurn) == SelectedPiece.Side && Util.InBounds(_newPos) && SelectedPiece.StoredMoves[_newPos.x, _newPos.y])
        {
            
            RemoveFromPosition(piece.initPos);
            if (GetAtPosition(_newPos) != null)
            {
                Piece p = GetAtPosition(_newPos);
                if (p.Side == piece.Side)
                {
                    FailMove();
                    return false;
                }
                if (p.Side != piece.Side)
                {
                    GetAtPosition(_newPos)?.Disable();
                }
            }
            SetIntoPosition(_newPos, SelectedPiece);
            SelectedPiece.currPos = _newPos;
            SelectedPiece.MoveTo(_newPos);
            EndTurn();
            return true;
        } 
        else 
        {
            FailMove();
            return false;
        }
    }

    private void FailMove()
    {
        SetIntoPosition(SelectedPiece.initPos, SelectedPiece);
        SelectedPiece.MoveTo(SelectedPiece.initPos);
    }

    void EndTurn()
    {
        ToggleTurn();
        checkManager.UpdateCurrentChecks();
        Deselect();
        DisableLegalCircles();
        Ended?.Invoke();
    }

    void ToggleTurn()
    {
        IsWhiteTurn = !IsWhiteTurn;
    }

    public void Select(Piece _piece)
    {
        EnableLegalCircles(_piece);
        SelectedPiece = _piece;
        PieceDeselected?.Invoke();
    }

    public void Deselect()
    {
        SelectedPiece?.OnDeselect();
        SelectedPiece = null;
        DisableLegalCircles();
    }


    void EnableLegalCircles(Piece _piece)
    {   
        DisableLegalCircles();
        for (int x = 0; x <= 13; x++)
        {
            for (int y = 0; y <= 13; y++)
            {
                if ( _piece.StoredMoves[x,y]) 
                {
                    if (Pieces[x,y] != null && Pieces[x,y].Side != _piece.Side) Grid.GetTileAtPosition(new Vector2(x,y))._legalSquare.gameObject.SetActive(true);
                    else Grid.GetTileAtPosition(new Vector2(x,y))._legalCircle.gameObject.SetActive(true);
                }
            }
        }
    }

    void DisableLegalCircles()
    {
        for (int x = 0; x <= 13; x++)
        {
            for (int y = 0; y <= 13; y++)
            {
                Grid.GetTileAtPosition(new Vector2(x,y))._legalCircle.gameObject.SetActive(false);
                Grid.GetTileAtPosition(new Vector2(x,y))._legalSquare.gameObject.SetActive(false);
            }
        }

    }
}

