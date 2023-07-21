using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public List<List<GameObject>> tiles;
    int a = 0;
    // Start is called before the first frame update
    void Start()
    {
        //Rook rook = new Rook("Black");
        //Tile tile = new Tile(rook,"b1");
        //tiles.Add(new List<Tile>() { new Tile(null, "a1"), tile, new Tile(null, "c1"), new Tile(new Rook("White"), "d1"), new Tile(null, "e1") });
        create_tiles();
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    void create_tiles()
    {
        tiles = new List<List<GameObject>>();
        foreach (Transform row in this.transform)
        {
            List<GameObject> tile_row = new List<GameObject>();
            foreach (Transform tile in row.transform)
            {
                tile.GetComponent<Tile>().pos = new List<int> { tiles.Count, tile_row.Count };
                tile_row.Add(tile.gameObject);
                tile.GetComponent<Tile>().is_set = true;
            }
            tiles.Add(tile_row);
        }
    }
}
