using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class tileDestroy : MonoBehaviour
{
    Tilemap tilemap;
    Vector3 mPos;
    private Vector3Int tilepos;
    [SerializeField] GridLayout grid;
    [SerializeField] Tile tile1swap;
    [SerializeField] Tile tile2swap;
    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        tilepos = grid.WorldToCell(mPos);
        if (Input.GetKeyDown(KeyCode.T))
            print(mPos + " " + tilepos);
        if (Input.GetKeyDown(KeyCode.G))
            tilemap.SetTile(tilepos, null);
    }
    public void DestroyTile(Vector3 pos, float x)
    {
        Vector3Int mPos = grid.WorldToCell(pos);
        //mPos += Vector3Int.down + Vector3Int.down;
        mPos += Vector3Int.down;
        //print(tilemap.GetTile(mPos).name + " " + mPos);
        //mPos.x -= (int)x;
        print(mPos);
        for (int i = 0; i <= 19; i++)
        {
            if (i == 4)
            {
                i = 15;
                continue;
            }
            if (tilemap.GetTile(mPos).name == "Tilemap_" + i)
            {
                //tilemap.SetTile(mPos, tile1swap);
                StartCoroutine(paintInBlood(mPos, tile1swap));
            }
            if (tilemap.GetTile(mPos - Vector3Int.left).name == "Tilemap_" + i)
            {
                //tilemap.SetTile(mPos - Vector3Int.left, tile1swap);
                StartCoroutine(paintInBlood(mPos - Vector3Int.left, tile1swap));
            }
            if (tilemap.GetTile(mPos + Vector3Int.left).name == "Tilemap_" + i)
            {
                //tilemap.SetTile(mPos + Vector3Int.left, tile1swap);
                StartCoroutine(paintInBlood(mPos + Vector3Int.left, tile1swap));
            }
        }
        for (int i = 0; i <= 19; i++)
        {
            if (i == 3)
            {
                i = 16;
                continue;
            }
            if (tilemap.GetTile(mPos).name == "TileMap_mayby_" + i)
                StartCoroutine(paintInBlood(mPos, tile2swap));
            if (tilemap.GetTile(mPos - Vector3Int.left).name == "TileMap_mayby_" + i)
                StartCoroutine(paintInBlood(mPos - Vector3Int.left, tile2swap));
            if (tilemap.GetTile(mPos + Vector3Int.left).name == "TileMap_mayby_" + i)
                StartCoroutine(paintInBlood(mPos + Vector3Int.left, tile2swap));
        }
    }
    public IEnumerator paintInBlood(Vector3Int _pos, Tile _t)
    {
        yield return new WaitForSeconds(0.6f);
        tilemap.SetTile(_pos, _t);
    }
}
