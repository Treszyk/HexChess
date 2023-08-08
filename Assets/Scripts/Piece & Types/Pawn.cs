using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Pawn : Piece
{
    public bool moved2spaces = false;
    public GameObject en_passante_tile = null;
    // Start is called before the first frame update
    void Start()
    {
        
        this.piece_name = "pawn";
        set_sprite();
        //GameObject test = new GameObject("rook");
        //test.AddComponent<Rook>();
        //test.AddComponent<SpriteRenderer>();
        //Instantiate(test);
    }
    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;
        int help_y = pos_y;
        //forward moves
        for(int i = 0;i<2;i++)
        {
            if (i == 1)
            {
                if((color == color.WHITE && pos_y >= board.tiles.Count / 2) || (color == color.BLACK && pos_y <= board.tiles.Count / 2) || !tile.is_start_pos)
                    break;
            }
            help_y = help_y + (color==color.WHITE ? 1 : -1);
            if (help_y > board.tiles.Count / 2 && color == color.WHITE)
                help_x--;
            else if (color == color.BLACK)
            {
                if(help_y < board.tiles.Count / 2)
                {
                    help_x += 0;
                }
                else help_x++;
            }
            if (board.is_in_bounds(help_y, help_x))
            {
                if(board.tiles[help_y][help_x].GetComponent<Tile>().piece is not null)
                    break;
            }
                

            legal_move_handler(help_y, help_x);
        }
        //side moves
        //add an if to check if a piece is on the tile
        int offset_right_w = 0;
        int offset_left_w = 0;
        int offset_right_b = 0;
        if (color==color.WHITE)
        {
            if(pos_y >= board.tiles.Count/2)
                offset_right_w = -1;
            if (pos_y > board.tiles.Count / 2)
                offset_left_w = 1;  
            if(board.tile_contains_piece(pos_y + 1, pos_x + 1 + offset_right_w))
                legal_move_handler(pos_y + 1, pos_x + 1 + offset_right_w);
            if(board.tile_contains_piece(pos_y, pos_x - 1))           
                legal_move_handler(pos_y, pos_x - 1);
            //en_passante white
            if (board.tile_contains_piece(pos_y, pos_x + 1))
            {
                //right-top enpassante
                Piece temp_piece = board.tiles[pos_y][pos_x + 1].GetComponent<Tile>().piece.GetComponent<Piece>();
                if (temp_piece.piece_name == "pawn" && temp_piece.color != this.color)
                    if (temp_piece.GetComponent<Pawn>().moved2spaces)
                    {
                        legal_move_handler(pos_y + 1, pos_x + 1 + offset_right_w, (int)Highlight.PURPLE);
                        en_passante_tile = temp_piece.transform.parent.transform.gameObject;
                        board.tiles[pos_y + 1][pos_x + 1 + offset_right_w].GetComponent<Tile>().en_passante = true;
                    }
            }
            if (board.tile_contains_piece(pos_y - 1, pos_x - 1 + offset_left_w))
            {
                //left-top enpassante
                Piece temp_piece = board.tiles[pos_y - 1][pos_x - 1 + offset_left_w].GetComponent<Tile>().piece.GetComponent<Piece>();
                if (temp_piece.piece_name == "pawn" && temp_piece.color != this.color)
                    if (temp_piece.GetComponent<Pawn>().moved2spaces)
                    {
                        legal_move_handler(pos_y, pos_x - 1, (int)Highlight.PURPLE);
                        en_passante_tile = temp_piece.transform.parent.transform.gameObject;
                        board.tiles[pos_y][pos_x - 1].GetComponent<Tile>().en_passante = true;
                    }
            }

        }
        else
        {
            
            if (pos_y <= (board.tiles.Count / 2))
                offset_right_w = 1;
            if (pos_y < board.tiles.Count / 2)
                offset_right_b = 1;

            if (board.tile_contains_piece(pos_y - 1, pos_x - offset_right_w))
                legal_move_handler(pos_y - 1, pos_x - offset_right_w);
            if (board.tile_contains_piece(pos_y, pos_x + 1))
                legal_move_handler(pos_y, pos_x + 1);
            //en passante black
            if (board.tile_contains_piece(pos_y + 1, pos_x + offset_right_b))
            {
                Piece temp_piece = board.tiles[pos_y + 1][pos_x + offset_right_b].GetComponent<Tile>().piece.GetComponent<Piece>();
                if (temp_piece.piece_name == "pawn")
                    if (temp_piece.GetComponent<Pawn>().moved2spaces)
                    {
                        legal_move_handler(pos_y, pos_x + 1, (int)Highlight.PURPLE);
                        en_passante_tile = temp_piece.transform.parent.transform.gameObject;
                        board.tiles[pos_y][pos_x + 1].GetComponent<Tile>().en_passante = true;
                    }
            }
            if (board.tile_contains_piece(pos_y, pos_x - 1))
            {
                Piece temp_piece = board.tiles[pos_y][pos_x - 1].GetComponent<Tile>().piece.GetComponent<Piece>();
                if (temp_piece.piece_name == "pawn")
                    if (temp_piece.GetComponent<Pawn>().moved2spaces)
                    {
                        legal_move_handler(pos_y - 1, pos_x - offset_right_b, (int)Highlight.PURPLE);
                        en_passante_tile = temp_piece.transform.parent.transform.gameObject;
                        board.tiles[pos_y - 1][pos_x - offset_right_b].GetComponent<Tile>().en_passante = true;
                    }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

