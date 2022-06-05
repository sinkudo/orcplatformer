using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float agroDist;
    protected bool facingRight = true;
    protected GameObject gameobjPlayer;
    [SerializeField] protected Player player;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = transform.position;
    }
    protected abstract void Attack();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(gameObject.name + " " + collision.gameObject.name);
        if (collision.gameObject.name == "Attack_Hitbox" && gameObject.layer == 6)
        {
            float push = player.getPlayerDirection() ? 2f : -2f;
            rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            hp -= player.getPlayerDamage();
        }
        if (hp <= 0)
            Destroy(gameObject);
    }
    
}
