using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string color = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public abstract void returnLegalMoves(Tile tile);

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setColor(string color)
    {
        this.color = color;
    }
}
