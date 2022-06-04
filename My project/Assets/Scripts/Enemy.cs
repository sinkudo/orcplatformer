using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    float hp = 100f;
    float damage = 20f;
    private GameObject gameobjPlayer;
    [SerializeField] private Player player;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Hitbox")
        {
            float push = (player.getPlayerDirection() ? 2f : -2f);
            rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            hp -= player.getPlayerDamage();
        }
        print(hp);
        if (hp <= 0)
            Destroy(gameObject);
    }
}
