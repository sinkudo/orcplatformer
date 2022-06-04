using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] protected float hp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float agroDist;
    protected bool facingRight = true;
    protected GameObject gameobjPlayer;
    [SerializeField] protected Player player;
    protected Rigidbody2D rb;
    protected Transform startPos;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = transform;
    }
    protected abstract void Move();
    protected abstract void Attack();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Hitbox")
        {
            float push = player.getPlayerDirection() ? 2f : -2f;
            rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            hp -= player.getPlayerDamage();
        }
        print(hp);
        if (hp <= 0)
            Destroy(gameObject);
    }
    protected abstract void StartHunting();
    protected abstract void StopHunting();
    //protected void Flip()
    //{
    //    facingRight = !facingRight;
    //    Vector3 thescale = transform.localScale;
    //    thescale.x *= -1;
    //    transform.localScale = thescale;
    //}
}
