using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Board board;
    public GameObject piece;
    public List<int> pos;
    public bool is_set = false;
    public Color basic_color;

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

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        board.clear_highlights();
        if (this.piece is not null)
        {
            this.piece.gameObject.GetComponent<Piece>().click_handler();
        }
    }
}
