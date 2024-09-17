using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private TextMeshPro locationText;
    [SerializeField] public GameObject _legalCircle, _legalSquare;
 
    public void Init(bool isOffset) {
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        locationText.SetText(transform.position.x+","+transform.position.y);
    }
 
    void OnMouseEnter() 
    {
        _highlight.SetActive(true);
        locationText.gameObject.SetActive(true);
    }
 
    void OnMouseExit()
    {
        _highlight.SetActive(false);
        locationText.gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        // Manager.Instance.OnTileClick(Vector2Int.RoundToInt(this.transform.position));
        Manager.Instance.Deselect();
    }
}
