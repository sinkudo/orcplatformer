using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPeasant : Enemy
{
    // Start is called before the first frame update
    // Update is called once per frame
    
    void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distToPlayer <= agroDist)
            StartHunting();
        //else
        //    StopHunting();
        if (Input.GetKeyDown(KeyCode.Space))
            print(distToPlayer + " " + startPos);
    }
    protected override void Move()
    {
               
    }

    protected override void Attack()
    {
        
    }
    protected override void StartHunting()
    {
        //float moveX = transform.position.x < player.transform.position.x ? -1f : 1f;
        float moveX = Destination(player.transform);
        Flip(player.transform);
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        //transform.localScale = new Vector2(transform.localScale.x * moveX, transform.localScale.y);
    }
    protected override void StopHunting()
    {
        float moveX = Destination(player.transform);
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        
        //transform.localScale = new Vector2(transform.localScale.x * moveX, transform.localScale.y);
    }
    protected float Destination(Transform target)
    {
        float movex = transform.position.x < target.position.x ? 1f : -1f;
        return movex;
    }
    protected void Flip(Transform target)
    {
        if (transform.position.x < target.position.x && transform.localScale.x == -1 ||
            transform.position.x > target.position.x && transform.localScale.x ==  1)
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
}
