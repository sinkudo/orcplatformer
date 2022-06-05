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
    protected SpriteRenderer sprite;
    [SerializeField] protected Player player;
    protected Rigidbody2D rb;
    protected Vector3 startPos;
    
    [SerializeField] private Material matBlink;
    [SerializeField] private Material matDefault;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = transform.position;
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    protected abstract void Attack();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Attack_Hitbox" && gameObject.layer == 6)
        {
            StartCoroutine(blink());
            float push = player.getPlayerDirection() ? 2f : -2f;
            rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            hp -= player.getPlayerDamage();
        }
        if (hp <= 0)
            Destroy(gameObject);
    }
    private IEnumerator blink()
    {
        sprite.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        sprite.material = matDefault;
    }
}
