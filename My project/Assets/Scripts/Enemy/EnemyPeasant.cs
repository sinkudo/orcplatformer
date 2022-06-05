using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeasant : MovingEnemy
{
    protected Vector3 startingPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = new Vector3(transform.position.x + 1, transform.position.y);
        patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
        animator = GetComponent<Animator>();
        
        print(transform.position);
        print(patrolPoint);
    }
    //void Update()
    //{
    //    float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
    //    if (distToPlayer <= agroDist)
    //        StartHunting();   
    //    else if ((int)startPos.x != (int)transform.position.x)
    //        StopHunting();
    //    Debug.DrawLine(transform.position - new Vector3(agroDist, 0), transform.position + new Vector3(agroDist, 0));
    //}
    //public override void Move(float _speed)
    //{
    //    rb.velocity = new Vector2(_speed, rb.velocity.y);
    //}

    protected override void Attack()
    {

    }
    //protected override void StartHunting()
    //{
    //    //float moveX = transform.position.x < player.transform.position.x ? -1f : 1f;
    //    float moveX = Destination(player.transform.position);
    //    Flip(player.transform.position);
    //    rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
    //    var d = player.transform.position - transform.position;
    //}
    //protected override void StopHunting()
    //{
    //    float moveX = Destination(startPos);
    //    rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
    //    print(rb.velocity);
    //    Flip(startPos);
    //}
    //protected float Destination(Vector3 target)
    //{
    //    float movex = transform.position.x < target.x ? 1f : -1f;
    //    return movex;
    //}
    //protected void Flip(Vector3 target)
    //{
    //    Vector3 enemypos = transform.position;
    //    if (enemypos.x < target.x && transform.localScale.x == -1 ||
    //        enemypos.x > target.x && transform.localScale.x == 1)
    //        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    //}
}
