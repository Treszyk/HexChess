using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum color
{
    WHITE = -1,
    BLACK = 1,
}
public enum Highlight
{
    CYAN = 0,
    PURPLE = 1,
}
public abstract class Piece : MonoBehaviour
{
    public color color;
    public Board board;
    public List<List<int>> legalMoves;
    public string piece_name;

    // Start is called before the first frame update
    void Start()
    {
        legalMoves = new List<List<int>>();  
    }
    public void click_handler()
    {
        legalMoves = new List<List<int>>();
        returnLegalMoves(transform.GetComponentInParent<Tile>());
        remove_illegal_moves();
        board.highlight_moves();
    }

    void remove_illegal_moves()
    {
        int pos_x = transform.parent.GetComponent<Tile>().pos[1];
        int pos_y = transform.parent.GetComponent<Tile>().pos[0];
        Vector3 position = new Vector3(200, 200, 200);
        Board temp_board = Instantiate(this.board, position, Quaternion.identity);
        //Debug.Log(temp_board.selected_tile);
        temp_board.create_tiles();
        temp_board.update_tiles();
        Piece king = temp_board.king_w;
        List<List<int>> illegal_moves = new List<List<int>>();

        foreach (List<int> pos in legalMoves)
        {
            temp_board.move_piece(temp_board.tiles[pos[0]][pos[1]], true);
            temp_board.selected_tile = temp_board.tiles[pos[0]][pos[1]];
            //if (temp_board.tiles[pos[0]][pos[1]].GetComponent<Tile>().piece.GetComponent<Piece>() == king)
            //{
            //    Debug.Log($"{king.transform.parent.GetComponent<Tile>().pos[0]} {king.transform.parent.GetComponent<Tile>().pos[1]}");
            //}
            List<List<int>> enemy_moves = temp_board.return_legal_moves_by_color(color == color.WHITE ? color.BLACK : color.WHITE);
            if (temp_board.is_king_in_check(enemy_moves, this.color))
            {
                illegal_moves.Add(pos);
            }
            temp_board.move_piece(temp_board.tiles[pos_y][pos_x], true);
            temp_board.selected_tile = temp_board.tiles[pos_y][pos_x];
            temp_board.update_tiles();
        }
        foreach (List<int> i in illegal_moves)
        {
            legalMoves.Remove(i);
        }
        Destroy(temp_board.gameObject);
    }

    public void set_sprite()
    {
        SpriteRenderer r = GetComponent<SpriteRenderer>();
        string color_prefix = color == color.WHITE ? "w_" : "b_";
        Sprite image = Resources.Load<Sprite>($"Sprites/{color_prefix}{piece_name}");
        transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
        r.sprite = image;
        r.color = Color.white;
        transform.localPosition = Vector3.zero;
    }
    public abstract void returnLegalMoves(Tile tile);
    public bool legal_move_handler(int y, int x)
    {
        if(y < 0 || y >= board.tiles.Count || x < 0 || x >= board.tiles[y].Count) return false;
        Tile move_tile = board.tiles[y][x].GetComponent<Tile>();
        if (move_tile.piece is null)
        {
            //move_tile.GetComponent<SpriteRenderer>().color = highlight;
            legalMoves.Add(new List<int> { y, x });
        }
        else if (move_tile.piece.GetComponent<Piece>().color != this.color)
        {
            //move_tile.GetComponent<SpriteRenderer>().color = highlight;
            legalMoves.Add(new List<int> { y, x });
            return false;
        }
        else if (move_tile.piece.GetComponent<Piece>().color == this.color)
        {
            return false;
        }
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
