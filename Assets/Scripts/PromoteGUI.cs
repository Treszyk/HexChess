using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PromoteGUI : MonoBehaviour
{
    public Piece chosen_piece;
    public bool is_choosing = false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (chosen_piece != null)
        {
            this.gameObject.SetActive(false);
            is_choosing = false;
            chosen_piece = null;
        }
    }

    public void set_promoting_pieces_color(color piece_color)
    {
        foreach(Transform piece in transform)
        {
            piece.GetComponent<Piece>().color = piece_color;
            piece.GetComponent<PromotingPiece>().change_color(piece_color);
        }
    }

}
