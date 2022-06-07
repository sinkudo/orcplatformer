using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //[SerializeField] protected float hp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float agroDist;
    protected bool facingRight = true;
    protected GameObject gameobjPlayer;
    protected SpriteRenderer sprite;
    [SerializeField] protected Player player;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    protected Health health;
    [SerializeField] private Material matBlink;
    [SerializeField] private Material matDefault;
    protected Health PlayerHealth;
    public bool PlayerInAttackRange = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
        health = GetComponent<Health>();
    }
    protected abstract void Attack();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Hitbox" && gameObject.layer == 6)
        {
            StartCoroutine(blink());
            float push = player.getPlayerDirection() ? 2f : -2f;
            rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            //print(player.getPlayerDamage());
            health.TakeDamage(1);
            //hp -= player.getPlayerDamage();
        }
        //if (hp <= 0)
        //    Destroy(gameObject);
    }
    private IEnumerator blink()
    {
        sprite.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        sprite.material = matDefault;
    }
    protected void DamagePlayer()
    {
        if (PlayerInAttackRange && !player.isInvincible)
            PlayerHealth.TakeDamage(damage);
    }
}
