using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int width, height;
 
    [SerializeField] private Tile tilePrefab;
 
    [SerializeField] private Transform board;
 
    private Dictionary<Vector2, Tile> tiles;
 
 
    void Start() {
        GenerateGrid();
    }
 
    void GenerateGrid() {
        tiles = new Dictionary<Vector2, Tile>();
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                var spawnedTile = Instantiate(tilePrefab, new Vector3(x, y, 3), Quaternion.identity, board);
                spawnedTile.name = $"Tile ({x},{y})";
 
                var isOffset = (x % 2 == 0 && y % 2 != 0) || (x % 2 != 0 && y % 2 == 0);
                spawnedTile.Init(isOffset);
 
 
                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
    }

    public Tile GetTileAtPosition(Vector2 pos) {
        if (tiles.TryGetValue(pos, out var tile)) return tile;
        return null;
    }
}
