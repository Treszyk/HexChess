using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Rook : Piece
{
 
    // Start is called before the first frame update
    void Start()
    {
        this.piece_name = "rook";
        set_sprite();
    }
    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;

        for (int x = pos_x - 1; x >= 0; x--)
        {
            if (!legal_move_handler(pos_y, x)) break;
        }
        for (int x = pos_x + 1; x < board.tiles[pos_y].Count; x++)
        {
            if (!legal_move_handler(pos_y, x)) break;
        }
        for (int y = pos_y + 1; y < board.tiles.Count; y++)
        {
            if (y > board.tiles.Count / 2)
            {
                help_x--;
            }
            if (!legal_move_handler(y, help_x))
                break;
        }
        help_x = pos_x;
        for (int y = pos_y + 1; y < board.tiles.Count; y++)
        {
            if (y <= board.tiles.Count / 2)
            {
                help_x++;
            }
            if (!legal_move_handler(y, help_x))
                break;
        }
        help_x = pos_x;
        for (int y = pos_y - 1; y >= 0; y--)
        {
            if (y < board.tiles.Count / 2)
            {
                help_x--;
            }
            if (!legal_move_handler(y, help_x))
                break;
        }
        help_x = pos_x;
        for (int y = pos_y - 1; y >= 0; y--)
        {
            if (y >= board.tiles.Count / 2)
            {
                help_x++;
            }
            if (!legal_move_handler(y, help_x))
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}

