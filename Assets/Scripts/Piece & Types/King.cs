using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

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
        Vector3 position = new Vector3(200,200,200);
       
        List<List<int>> illegal_moves = new List<List<int>>();
        
        foreach (List<int> pos in legalMoves)
        {
            //temporary fix, try to find out how to reset the king's position
            Board temp_board = Instantiate(this.board, position, Quaternion.identity);
            Debug.Log(temp_board.selected_tile);
            temp_board.create_tiles();
            temp_board.update_tiles();
            Piece king = temp_board.king_w;
            temp_board.move_piece(temp_board.tiles[pos[0]][pos[1]]);
            temp_board.selected_tile = temp_board.tiles[pos[0]][pos[1]];
            //if (temp_board.tiles[pos[0]][pos[1]].GetComponent<Tile>().piece.GetComponent<Piece>() == king)
            //{
            //    Debug.Log($"{king.transform.parent.GetComponent<Tile>().pos[0]} {king.transform.parent.GetComponent<Tile>().pos[1]}");
            //}
            List<List<int>> enemy_moves = temp_board.return_legal_moves_by_color(color == color.WHITE ? color.BLACK : color.WHITE);
            if (temp_board.is_king_in_check(enemy_moves, this.color))
            {
                illegal_moves.Add(pos);
            }
            Destroy(temp_board.gameObject);
        }
        foreach(List<int> i in illegal_moves)
        {
            legalMoves.Remove(i);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
    }
}

