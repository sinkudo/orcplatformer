using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    public int Coins;
    void Start()
    {
        //player = GetComponentInParent<Player>();
        //if(Coins <= 0)
        //    Coins = 0;
        if (!PlayerPrefs.HasKey("coins"))
            Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            Coins++;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Coins++;
            Destroy(collision.gameObject);
        }
    }
    public void LoadCoin(int _c)
    {
        print(_c);
        Coins = _c;
    }
}
