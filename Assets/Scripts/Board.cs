using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;



public class Board : MonoBehaviour
{
    public List<List<GameObject>> tiles;
    public List<List<int>> global_legal_moves;
    public GameObject selected_tile;
    public TextMeshProUGUI tmp;
    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 60;
        //Rook rook = new Rook("Black");
        //Tile tile = new Tile(rook,"b1");
        //tiles.Add(new List<Tile>() { new Tile(null, "a1"), tile, new Tile(null, "c1"), new Tile(new Rook("White"), "d1"), new Tile(null, "e1") });
        create_tiles();
        global_legal_moves = new List<List<int>>();
        selected_tile = null;
    }

    public void move_piece(GameObject move_tile)
    {
        selected_tile.GetComponent<Tile>().piece.transform.parent = move_tile.transform;
        selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().set_sprite();
        move_tile.GetComponent<Tile>().piece = selected_tile.GetComponent<Tile>().piece;
        selected_tile.GetComponent<Tile>().piece = null;
    }
    // Update is called once per frame
    void Update()
    {
    }

    public bool is_in_bounds(int y, int x )
    {
        if (y >= 0 && y < tiles.Count)
        {
            if (x >= 0 && x < tiles[y].Count)
                return true;
        }
        return false;
    }

    public bool tile_contains_piece(int y, int x)
    {
        if(is_in_bounds(y, x))
            return tiles[y][x].GetComponent<Tile>().contains_piece();
        return false;
    }

    void create_tiles()
    {
        tiles = new List<List<GameObject>>();
        foreach (Transform row in this.transform)
        {
            List<GameObject> tile_row = new List<GameObject>();
            foreach (Transform tile in row.transform)
            {
                tile.GetComponent<Tile>().pos = new List<int> { tiles.Count, tile_row.Count };
                tile_row.Add(tile.gameObject);
            }
            tiles.Add(tile_row);
        }
    }
    public void clear_highlights()
    {
        foreach (List<GameObject> row in tiles)
        {
            foreach(GameObject tile in row)
            {
                tile.GetComponent<SpriteRenderer>().color = tile.GetComponent<Tile>().basic_color;
            }
        }
    }
}
