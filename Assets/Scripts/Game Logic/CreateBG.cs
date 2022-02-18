using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateBG : MonoBehaviour
{
    public List<TileBase> BackgroundTiles = new List<TileBase>();

    public int startX = -10;
    public int startY = -10;

    public int w = 20;
    public int h = 20;

    Tilemap tilemap;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GameObject.Find("Tilemap")?.GetComponent<Tilemap>();

        if (tilemap is null)
        {
            Debug.LogError("Couldn't find Tilemap");

            Destroy(this);

            return;
        }

        var pos = Vector3Int.zero;

        for (int y = startY; y < startY+h; y++)
        {
            for (int x = startX; x < startX + w; x++)
            {
                pos.x = x;
                pos.y = y;

                tilemap.SetTile(
                    pos,
                    BackgroundTiles[
                        Random.Range(0,BackgroundTiles.Count)
                    ]);
            }
        }
    }
}
