using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingEnemy : Enemy
{
    protected Vector3 patrolPoint;
    protected Animator animator;
    protected float moveHorizontal = 1;
    protected bool chasing = false;
    public bool mustGoHome = false;
    protected float stayTime = 0f;
    public bool reachedPoint = false;
    public bool inSight = false;
    protected override void Attack()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //gameobjPlayer = GameObject.Find("Player");
        //player = gameobjPlayer.GetComponent<Player>();
        //startPos = new Vector3(transform.position.x + 1, transform.position.y);
        //patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (reachedPoint && !inSight)
        {
            stayTime += Time.deltaTime;
            animator.SetTrigger("Enemy_Idle");
            rb.velocity = Vector2.zero;
            if (stayTime >= 1f)
            {
                stayTime = 0f;
                reachedPoint = false;
            }
            return;
        }
        float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
        //print(transform.position + " " + player.transform.position + " " + agroDist + " " + distToPlayer);
        //if (distToPlayer <= agroDist)
        //    StartHunting();
        //else if (mustGoHome)
        //    backToStartPos();
        //else if (!chasing && !reachedPoint && stayTime <= 0f)
        //    Patrol();
        if (inSight)
            StartHunting();
        else if (mustGoHome)
            backToStartPos();
        else Patrol();
        //Debug.DrawLine(transform.position - new Vector3(agroDist, 0), transform.position + new Vector3(agroDist, 0));
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(patrolPoint, 1);
    }
    public void Move(float _speed)
    {
        rb.velocity = new Vector2(_speed, rb.velocity.y);
    }
    protected void StartHunting()
    {
        animator.SetTrigger("Enemy_Move");
        chasing = true;
        float moveX = Destination(player.transform.position);
        moveHorizontal = moveX;
        Flip(player.transform.position);
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        var d = player.transform.position - transform.position;
        mustGoHome = true;
    }
    protected void backToStartPos()
    {
        print("home");
        if((int)Vector2.Distance(transform.position, startPos) <= 2)
        {
            mustGoHome = false;
            return;
        }
        //print( startPos.x + " " +transform.position.x);
        chasing = false;
        float moveX = Destination(startPos);
        moveHorizontal = moveX;
        
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        Flip(startPos);
        
        //Flip();
    }
    protected float Destination(Vector3 target)
    {
        float movex = transform.position.x < target.x ? 1f : -1f;
        return movex;
    }
    protected void Flip(Vector3 target)
    {
        Vector3 enemypos = transform.position;
        if ((int)target.x != (int)transform.position.x)
            if (enemypos.x < target.x && transform.localScale.x == -1 ||
                enemypos.x > target.x && transform.localScale.x == 1)
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    protected void Patrol()
    {
        //print("patrol");
        //print(transform.position + " " + patrolPoint);
        if ((int)Vector2.Distance(transform.position, patrolPoint) <= 1)
        {
            reachedPoint = true;
            patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
            return;
        }
        animator.SetTrigger("Enemy_Move");
        float moveX = Destination(patrolPoint);
        moveHorizontal = moveX;
        //print(moveHorizontal);
        Move(speed * moveHorizontal);
        Flip(patrolPoint);
        //Flip();
    }
}
