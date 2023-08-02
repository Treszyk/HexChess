using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Pawn : Piece
{

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
        int offset_right = 0;
        if (color==color.WHITE)
        {
            if(pos_y >= board.tiles.Count/2)
                offset_right = -1;
            
            if(board.tile_contains_piece(pos_y + 1, pos_x + 1 + offset_right))
                legal_move_handler(pos_y + 1, pos_x + 1 + offset_right);
            if(board.tile_contains_piece(pos_y, pos_x - 1))           
                legal_move_handler(pos_y, pos_x - 1);
        } else
        {
            
            if (pos_y <= (board.tiles.Count / 2))
                offset_right = 1;

            if (board.tile_contains_piece(pos_y - 1, pos_x - offset_right))
                legal_move_handler(pos_y - 1, pos_x - offset_right);
            if (board.tile_contains_piece(pos_y, pos_x + 1))
                legal_move_handler(pos_y, pos_x + 1);
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}

