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
    public List<Piece> pieces;
    int turn = (int)color.WHITE;
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
        StartCoroutine(set_pieces_timer());
    }
    IEnumerator set_pieces_timer()
    {
        yield return new WaitForSeconds(0.0000001f);
        set_pieces();
    }
    public void set_pieces()
    {
        pieces.Clear();
        foreach (List<GameObject> row in tiles)
        {
            List<GameObject> tile_row = new List<GameObject>();
            foreach (GameObject tile in row)
            {
                if(tile.GetComponent<Tile>().piece != null)
                    pieces.Add(tile.GetComponent<Tile>().piece.GetComponent<Piece>());
            }
        }
    }
    public void move_piece(GameObject move_tile)
    {
        bool is_pawn = false;

        if (selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().piece_name == "pawn")
             is_pawn = true;
        foreach (Piece piece in pieces)
        {
            //Debug.Log(piece.transform.parent);
            //Debug.Log(piece.transform.parent.GetComponent<Tile>().piece);
            if (piece.piece_name == "pawn")
                piece.transform.parent.GetComponent<Tile>().piece.GetComponent<Pawn>().moved2spaces = false;
        }

        string piece_name = selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().piece_name;
        Tile _selected_tile = selected_tile.GetComponent<Tile>();
        Tile _move_tile = move_tile.GetComponent<Tile>();

        //check if pawn has moved 2 squares in current move
        if (piece_name == "pawn" && (math.abs(_selected_tile.pos[0] - _move_tile.pos[0]) == 2)) {
            selected_tile.GetComponent<Tile>().piece.GetComponent<Pawn>().moved2spaces = true;
        }
        
        _selected_tile.piece.transform.parent = move_tile.transform;
        _selected_tile.piece.GetComponent<Piece>().set_sprite();
        //check if pawn is moving into the promoting hex in current move
        if (move_tile.GetComponent<Tile>().promoting && is_pawn)
        {
            GameObject promoting_piece = new GameObject("rook");
            promoting_piece.AddComponent<Rook>();
            promoting_piece.AddComponent<SpriteRenderer>();

            _move_tile.piece = promoting_piece;   

            promoting_piece.transform.parent = _move_tile.transform;
            promoting_piece.GetComponent<Piece>().board = this;
            promoting_piece.GetComponent<Piece>().color = _selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().color;

            _move_tile.piece.GetComponent <Piece>().set_sprite();
            Destroy(selected_tile.GetComponent<Tile>().piece);
        } else
        {
            _move_tile.piece = selected_tile.GetComponent<Tile>().piece;
        }
        _selected_tile.piece = null;

        set_pieces();
        this.turn *= -1;
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
                tile.GetComponent<Tile>().GetComponent<SpriteRenderer>().sortingOrder = -100;
                tile_row.Add(tile.gameObject);
            }
            tiles.Add(tile_row);
        }
        this.set_pieces();
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
