using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWolf : MovingEnemy
{
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = new Vector3(transform.position.x + 1, transform.position.y);
        patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        groundPoint = transform.Find("groundPoint");
        health = GetComponent<Health>();
        PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
    }

}
