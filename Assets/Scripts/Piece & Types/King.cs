using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class King : Piece
{

    // Start is called before the first frame update
    void Start()
    {
        this.piece_name = "king";
        set_sprite();
    }
    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;
        {


            legal_move_handler(pos_y, pos_x - 1);
            legal_move_handler(pos_y, pos_x + 1);
            if (pos_y + 1 > board.tiles.Count / 2)
            {
                help_x--;
            }
            legal_move_handler(pos_y + 1, help_x);
            help_x = pos_x;
            if (pos_y + 1 <= board.tiles.Count / 2)
            {
                help_x++;
            }
            legal_move_handler(pos_y + 1, help_x);
            help_x = pos_x;
            if (pos_y - 1 < board.tiles.Count / 2)
            {
                help_x--;
            }
            legal_move_handler(pos_y - 1, help_x);
            help_x = pos_x;
            if (pos_y - 1 >= board.tiles.Count / 2)
            {
                help_x++;
            }

            legal_move_handler(pos_y - 1, help_x);
            help_x = pos_x;

            help_x -= pos_y + 1 <= board.tiles.Count / 2 ? 1 : 2;
            legal_move_handler(pos_y + 1, help_x);
            help_x = pos_x;

            help_x -= pos_y - 1 >= board.tiles.Count / 2 ? 1 : 2;
            legal_move_handler(pos_y - 1, help_x);
            help_x = pos_x;

            help_x += pos_y + 1 <= board.tiles.Count / 2 ? 2 : 1;
            legal_move_handler(pos_y + 1, help_x);
            help_x = pos_x;

            help_x += pos_y - 1 < board.tiles.Count / 2 ? 1 : 2;
            legal_move_handler(pos_y - 1, help_x);
            help_x = pos_x;

            if (pos_y + 2 == (board.tiles.Count / 2) + 1) help_x += 0;
            else help_x += pos_y + 2 > board.tiles.Count / 2 ? -1 : 1;
            legal_move_handler(pos_y + 2, help_x);
            help_x = pos_x;

            if (pos_y - 2 == (board.tiles.Count / 2) - 1) help_x += 0;
            else help_x += pos_y - 2 >= board.tiles.Count / 2 ? 1 : -1;
            legal_move_handler(pos_y - 2, help_x);
        }
        //ruch
        

    }
    // Update is called once per frame
    void Update()
    {
    }
}

