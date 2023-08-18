using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PromotingPiece : Piece
{
    public Main main;
    public PromoteGUI promote;
    public override void returnLegalMoves(Tile tile)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void change_color(color c_color)
    {
        color = c_color;
        Vector3 pos = transform.position;
        Vector3 scale = transform.localScale;
        set_sprite();
        transform.localScale = scale;
        transform.position = pos;    
    }

    private void OnMouseDown()
    {
        Debug.Log("test");
        promote.chosen_piece = this;
        GameObject promoting_piece = new GameObject("piece");
        board.promoting_pawn_tile.GetComponent<Tile>().piece = promoting_piece;
 
        switch (this.piece_name)
        {
            case "queen":
                promoting_piece.AddComponent<Queen>();
                promoting_piece.GetComponent<Piece>().piece_name = "queen";
                break;
            case "rook":
                promoting_piece.AddComponent<Rook>();
                promoting_piece.GetComponent<Piece>().piece_name = "rook";
                break;
            case "bishop":
                promoting_piece.AddComponent<Bishop>();
                promoting_piece.GetComponent<Piece>().piece_name = "bishop";
                break;
            case "knight":
                promoting_piece.AddComponent<Knight>();
                promoting_piece.GetComponent<Piece>().piece_name = "knight";
                break;
        }
        promoting_piece.AddComponent<SpriteRenderer>();

        board.promoting_pawn_tile.GetComponent<Tile>().piece = promoting_piece;

        promoting_piece.transform.parent = board.promoting_pawn_tile.transform;
        promoting_piece.GetComponent<Piece>().board = board;
        promoting_piece.GetComponent<Piece>().color = (color)main.turn;

        board.promoting_pawn_tile.GetComponent<Tile>().piece.GetComponent<Piece>().set_sprite();
        //Destroy(board.selected_tile.GetComponent<Tile>().piece);
        board.set_pieces();
        main.turn *= -1;
    }

}
