using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    [SerializeField] public int width, height;
    [SerializeField] private Tile grassTile, mountainTile;
    
    private Dictionary<Vector2, Tile> tiles = new Dictionary<Vector2, Tile>();

    void Awake() {
        Instance = this;
    }
    public void GenerateGrid(){
        for (int x = 0; x < width; x++){
            for (int y = 0; y < height; y++){
                // var randomTile = Random.Range(0, 6) == 3 ? mountainTile : grassTile;
                var spawnedTile = Instantiate(grassTile, new Vector3(x * 120, y * 120), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                spawnedTile.Init(x, y);

                tiles[new Vector2(x, y)] = spawnedTile;
            }
        }
        
        CameraManager.Instance.ResetCamera();

        GameManager.Instance.ChangeState(GameState.SpawnParty);
    }

    public Tile GetHeroSpawnTile(int x, int y){
        return GetTileAtPosition(x, y);
    }

    public Tile GetEnemySpawnTile(){
        return tiles.Where(t => t.Key.x > width / 2 && t.Value.Walkable).OrderBy(t => Random.value).First().Value;
    }

    public Tile GetTileAtPosition(float x, float y){
        if (tiles.TryGetValue(new Vector2(x,y), out var tile)){
            return tile;
        }

        return null;
    }

    public void HideInRange(){
        foreach(Tile tile in tiles.Values){
            tile.SetRangeBool(false);
        }
    }

    /// <summary>
    /// Sets every tile around a tile given the range to InRange = True. This is so that tiles highlight red and the player can click on it.
    /// </summary>
    /// <param name="range">The range of the character's action</param>
    /// <param name="currentTile">The current tile the selected character is standing on</param>
    public void TilesInRange(int range, string currentTile){
        string[] segments = currentTile.Split(" ");
        int tileX = int.Parse(segments[1]);
        int tileY = int.Parse(segments[2]);
        Tile temp;
        
        HideInRange();

        for (int x = -range; x <= range; x++){
            for (int y = -range; y <= range; y++){
                Debug.Log($"{tileX + x}, {tileY + y}");
                temp = GetTileAtPosition(tileX + x, tileY + y); 
                if (temp != null) temp.SetRangeBool(true);
            }   
        }
    }
}
