using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Knight : Piece
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;
        int help_y = pos_y;

        List<List<int>> offsets = new()
        {
            new List<int>{-3, 2},
            new List<int>{-3, 1},
            new List<int>{-2, -1},

            //new List<int>{2,1},
            //new List<int>{1,2},

            //new List<int>{-1,2},
            //new List<int>{-2,1},

            //new List<int> {-3,-1},
            //new List<int> {-3,-2},

            //new List<int> {-2,-3},
            //new List<int> {-1,-3},

            //new List<int> {1,-3},
            //new List<int> {2,-3},
        };

        for(int i = 1; i <= 3; i++) {
            int special_offset = 0;
            //DO IT IF THE PIECE IS BELOW OR AT THE MIDDLE ROW       
            if(pos_y <= board.tiles.Count/2)
            {    
                //BOTTOM MOVES
                legal_move_handler(pos_y - i, pos_x + offsets[i - 1][0]);
                legal_move_handler(pos_y - i, pos_x + offsets[i - 1][1]);
                //TOP MOVES
                if (pos_y == (board.tiles.Count / 2) - 2 && i == 3)
                {
                    special_offset = 1; 
                } else if (pos_y == (board.tiles.Count/2) - 1)
                {
                    if (i == 2)
                        special_offset = 1;
                    else if(i == 3)
                        special_offset = 2;
                } else if (pos_y == (board.tiles.Count/2))
                {
                    special_offset = i;
                }
                legal_move_handler(pos_y + i, pos_x + Math.Abs(offsets[i - 1][0]) - special_offset);
                legal_move_handler(pos_y + i, pos_x + (offsets[i - 1][1] * (-1)) - special_offset);
            }
            

        }

    }
    // Update is called once per frame
    void Update()
    {

    }
}
