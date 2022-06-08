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
        Coins = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            Coins++;
            Destroy(collision.gameObject);
        }
    }
}
