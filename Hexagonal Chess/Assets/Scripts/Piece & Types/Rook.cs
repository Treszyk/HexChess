using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Rook : Piece
{
    public Board board;
    public List<List<int>> legalMoves;

    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];

        legalMoves.Clear();
        for (int x = pos_x - 1; x >= 0; x--)
        {

            Tile move_tile = board.tiles[pos_y][x].GetComponent<Tile>();
            if (move_tile.piece is null)
            {
                move_tile.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else if (move_tile.piece.GetComponent<Piece>().color != this.color)
            {
                move_tile.GetComponent<SpriteRenderer>().color = Color.cyan;
                Debug.Log($"this.color {this.color}, move_piece.color {move_tile.piece.GetComponent<Piece>().color}");
                break;
            }
            else if (move_tile.piece.GetComponent<Piece>().color == this.color)
            {
                break;
            }
        }
        for (int x = pos_x + 1; x < board.tiles[pos_y].Count ; x++)
        {

            Tile move_tile = board.tiles[pos_y][x].GetComponent<Tile>();
            if (move_tile.piece is null)
            {
                move_tile.GetComponent<SpriteRenderer>().color = Color.cyan;
            }
            else if (move_tile.piece.GetComponent<Piece>().color != this.color)
            {
                move_tile.GetComponent<SpriteRenderer>().color = Color.cyan;
                Debug.Log($"this.color {this.color}, move_piece.color {move_tile.piece.GetComponent<Piece>().color}");
                break;
            }
            else if (move_tile.piece.GetComponent<Piece>().color == this.color)
            {
                break;
            }
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButton(0))
        {
            Debug.Log(transform.GetComponentInParent<Tile>().is_set);
            legalMoves = new List<List<int>>();
            Debug.Log($"y: {transform.GetComponentInParent<Tile>().pos[0]} x: {transform.GetComponentInParent<Tile>().pos[1]}");
            returnLegalMoves(transform.GetComponentInParent<Tile>());
            foreach (var legalMove in legalMoves)
            {
                Debug.Log($"{legalMove[0]},{legalMove[1]}");
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
    }
    public Rook(string color)
    {
        setColor(color);
    }
}
