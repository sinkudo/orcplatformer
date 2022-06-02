using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float hp = 100f;
    float damage = 20f;
    [SerializeField] private Player player;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Hitbox")
        {
            print("hit");
            hp -= player.getPlayerDamage();
        }
        print(hp);
        if (hp <= 0)
            Destroy(gameObject);
    }
}
