using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    //[SerializeField] protected float hp;
    [SerializeField] protected float damage;
    [SerializeField] protected float speed;
    protected bool facingRight = true;
    protected GameObject gameobjPlayer;
    protected SpriteRenderer sprite;
    [SerializeField] protected Player player;
    public Rigidbody2D rb;
    protected Vector3 startPos;
    protected Health health;
    [SerializeField] private Material matBlink;
    [SerializeField] private Material matDefault;
    protected Health PlayerHealth;
    public bool PlayerInAttackRange = false;
    public bool canDamage = false;
    public bool isDead = false;
    [SerializeField] public Transform groundPoint;
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
        if (isDead)
            return;
        if (collision.gameObject.name == "Attack_Hitbox" && gameObject.layer == 6)
        {
            StartCoroutine(blink());
            print("damage take");
            float push = player.getPlayerDirection() ? 2f : -2f;
            rb.AddForce(new Vector2(push, 1f), ForceMode2D.Impulse);
            health.TakeDamage(player.getPlayerDamage());
        }
    }
    private IEnumerator blink()
    {
        sprite.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        sprite.material = matDefault;
    }
    private IEnumerator playerblink()
    {
        player.sprite.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        player.sprite.material = matDefault;
    }
    protected float Destination(Vector3 target)
    {
        float movex = transform.position.x < target.x ? 1f : -1f;
        return movex;
    }
    protected void DamagePlayer()
    {
        print(PlayerInAttackRange + " " + !player.isInvincible + " " + canDamage);
        if (PlayerInAttackRange && !player.isInvincible && canDamage)
        {
            //StartCoroutine(playerblink());
            StartCoroutine(player.blink());
            float push = Mathf.Sign(Destination(player.transform.position)) * 2f;
            player.rb.AddForce(new Vector2(push, 4f), ForceMode2D.Impulse);
            PlayerHealth.TakeDamage(damage);
        }
    }
    public void checkDeath()
    {
        if (isDead)
            Destroy(gameObject);
    }
    public void stopMovement()
    {
        rb.velocity = Vector2.zero;
    }
}
