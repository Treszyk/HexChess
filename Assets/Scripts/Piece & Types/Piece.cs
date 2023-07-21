using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum color
{
    WHITE,
    BLACK
}
public abstract class Piece : MonoBehaviour
{
    public color color;
    public Board board;
    public List<List<int>> legalMoves;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void click_handler()
    {
        board.selected_piece = this.gameObject;
        legalMoves = new List<List<int>>();

        returnLegalMoves(transform.GetComponentInParent<Tile>());
    }
    public abstract void returnLegalMoves(Tile tile);
    public bool legal_move_handler(int y, int x)
    {
        Tile move_tile = board.tiles[y][x].GetComponent<Tile>();
        if (move_tile.piece is null)
        {
            move_tile.GetComponent<SpriteRenderer>().color = UnityEngine.Color.cyan;
            legalMoves.Add(new List<int> { y, x });
        }
        else if (move_tile.piece.GetComponent<Piece>().color != this.color)
        {
            move_tile.GetComponent<SpriteRenderer>().color = UnityEngine.Color.cyan;
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
