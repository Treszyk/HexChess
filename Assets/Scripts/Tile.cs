using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Board board;
    public GameObject piece;
    public List<int> pos;
    public bool is_start_pos = false;
    public Color basic_color;
    public bool promoting = false;

    // Start is called before the first frame update
    void Start()
    {
        basic_color = GetComponent<SpriteRenderer>().color;
        if (transform.childCount > 0)
        {
            piece = transform.GetChild(0).gameObject;
        }
        else
        {
            piece = null;
        }

    }

    public bool contains_piece()
    {
        return piece is not null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        
        board.clear_highlights();
        
        if(this.piece is not null)
        {
            if(board.selected_tile is null)
            {
                board.selected_tile = this.gameObject;
                this.piece.gameObject.GetComponent<Piece>().click_handler();
            } 
            else if(board.selected_tile.GetComponentInChildren<Piece>().color == this.piece.GetComponent<Piece>().color)
            {
                board.selected_tile = this.gameObject;
                this.piece.gameObject.GetComponent<Piece>().click_handler();
            }
            else
            {
                foreach (List<int> move_pos in board.selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().legalMoves)
                {
                    if (pos[0] == move_pos[0] && pos[1] == move_pos[1])
                    {
                        Destroy(this.piece.gameObject);
                        board.move_piece(this.gameObject);

                        break;
                    }
                }
                board.selected_tile = null;
            }


        }
        else if (this.piece is null && board.selected_tile is not null)
        {
            foreach (List<int> move_pos in board.selected_tile.GetComponent<Tile>().piece.GetComponent<Piece>().legalMoves)
            {
                if (pos[0] == move_pos[0] && pos[1] == move_pos[1])
                {
                    board.move_piece(this.gameObject);
                    break;
                }
            }
            board.selected_tile = null;
        }
    }
}
