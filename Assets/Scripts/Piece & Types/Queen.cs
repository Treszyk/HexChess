using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Queen : Piece
{

    // Start is called before the first frame update
    void Start()
    {
        this.piece_name = "queen";
        set_sprite();
    }
    public override void returnLegalMoves(Tile tile)
    {
        legalMoves.Clear();
        int pos_x = tile.pos[1];
        int pos_y = tile.pos[0];
        int help_x = pos_x;

        Rook rook = gameObject.AddComponent<Rook>();
        rook.board = this.board;
        rook.legalMoves = new List<List<int>>();
        rook.returnLegalMoves(tile);
        Bishop bishop = gameObject.AddComponent<Bishop>();
        bishop.board = this.board;
        bishop.legalMoves = new List<List<int>>();
        bishop.returnLegalMoves(tile);

        foreach(List<int> pos in rook.legalMoves) {
            legalMoves.Add(pos);
        }
        foreach (List<int> pos in bishop.legalMoves)
        {
            legalMoves.Add(pos);
        }
        Destroy(rook);
        Destroy(bishop);   
    }

    // Update is called once per frame
    void Update()
    {
    }
}

