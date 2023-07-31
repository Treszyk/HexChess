using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bishop : Piece
{
    // Start is called before the first frame update
    void Start()
    {
        this.piece_name = "bishop";
        set_sprite();
    }

    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;

        //left-top moves
        for(int y = pos_y + 1; y < board.tiles.Count; y++)
        {
            help_x -= y <= board.tiles.Count / 2 ? 1 : 2;
            if (!legal_move_handler(y, help_x)) break;
        }
        help_x = pos_x;
        //left-bottom moves
        for (int y = pos_y - 1; y >= 0; y--)
        {
            help_x -= y >= board.tiles.Count / 2 ? 1 : 2;
            if (!legal_move_handler(y, help_x)) break;
        }
        help_x = pos_x;
        //top-right moves
        for(int y = pos_y + 1; y < board.tiles.Count; y++)
        {
            help_x += y <= board.tiles.Count / 2 ? 2 : 1;
            if (!legal_move_handler(y, help_x)) break;
        }
        help_x = pos_x;
        //top-bottom moves
        for (int y = pos_y - 1; y >= 0; y--)
        {
            help_x += y < board.tiles.Count / 2 ? 1 : 2;
            if (!legal_move_handler(y, help_x)) break;
        }
        help_x = pos_x;
        //xd
        for(int y = pos_y + 2; y < board.tiles.Count;y+=2)
        {
            if (y == (board.tiles.Count / 2) + 1) help_x += 0;
            else help_x += y > board.tiles.Count / 2 ? -1 : 1;

            if (!legal_move_handler(y, help_x)) break;
        }
        help_x = pos_x;
        for (int y = pos_y - 2; y >= 0; y -= 2)
        {
            if (y == (board.tiles.Count / 2) - 1) help_x += 0;
            else help_x += y >= board.tiles.Count / 2 ? 1 : -1;
            if (!legal_move_handler(y, help_x)) break;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
