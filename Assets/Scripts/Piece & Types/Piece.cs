using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum color
{
    WHITE,
    BLACK,
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
    public bool legal_move_handler(int y, int x, int highlight_c = (int)Highlight.CYAN)
    {
        Color highlight;
        if (highlight_c == (int)Highlight.CYAN)
            highlight = Color.cyan; 
        else 
            highlight = Color.magenta;

        if(y < 0 || y >= board.tiles.Count || x < 0 || x >= board.tiles[y].Count) return false;
        Tile move_tile = board.tiles[y][x].GetComponent<Tile>();
        if (move_tile.piece is null)
        {
            move_tile.GetComponent<SpriteRenderer>().color = highlight;
            legalMoves.Add(new List<int> { y, x });
        }
        else if (move_tile.piece.GetComponent<Piece>().color != this.color)
        {
            move_tile.GetComponent<SpriteRenderer>().color = highlight;
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
