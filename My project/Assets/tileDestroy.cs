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
        print(tilepos);
        if (Input.GetKeyDown(KeyCode.G))
            tilemap.SetTile(tilepos, null);
    }
}
