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
    public bool isAttacking = false;
    public bool justAttacked = false;
    private float attackDelay = 1f;
    protected float currentDelay = 0f;
    [SerializeField] GameObject Attack_Hitbox;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask ground;
    protected bool isGrounded = true;
    protected bool shouldTurn = false;
    public bool isInWall = false;
    [SerializeField] protected float attackAnimationTime;
    //protected bool isInfrontCliff = false;

    protected override void Attack()
    {
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameobjPlayer = GameObject.Find("Player");
        player = gameobjPlayer.GetComponent<Player>();
        startPos = new Vector3(transform.position.x + 1, transform.position.y);
        //patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
        spawnNewPoint(-5, 5);
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        groundPoint = transform.Find("groundPoint");
        health = GetComponent<Health>();
    }
    void Update()
    {
        print(justAttacked);
        checkGround();
        if (!isGrounded)
            return;
        if(PlayerInAttackRange && !justAttacked)
        {
            StartCoroutine(doAttack());
            return;
        }
        if (justAttacked)
        {
            afterAttackChill();
            return;
        }
        if (reachedPoint && !inSight)
        {
            afterReachPointChill();
            return;
        }
        if (inSight && !PlayerInAttackRange)
        {
            if (isCliff())
                return;
            StartHunting();
        }
        else if (mustGoHome)
            backToStartPos();
        else Patrol();
    }
    protected void afterReachPointChill()
    {
        stayTime += Time.deltaTime;
        animator.SetTrigger("Enemy_Idle");
        rb.velocity = Vector2.zero;
        if (stayTime >= 1f)
        {
            stayTime = 0f;
            reachedPoint = false;

            if (shouldTurn)
            {
                Flip();
                isInWall = false;
                shouldTurn = false;
            }
        }
    }
    protected void afterAttackChill()
    {
        currentDelay += Time.deltaTime;
        if (currentDelay >= attackDelay)
        {
            justAttacked = false;
            currentDelay = 0;
        }
    }
    protected IEnumerator doAttack()
    {
        //print("attack");
        animator.SetTrigger("Enemy_Attack");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(attackAnimationTime);
        animator.SetTrigger("Enemy_Idle");
        isAttacking = false;
        justAttacked = true;
        PlayerInAttackRange = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(patrolPoint, 0.3f);
        float moveX = transform.localScale.x;
        Gizmos.DrawSphere(groundPoint.position + new Vector3(moveX, 0), 0.2f);
    }
    public void Move(float _speed)
    {
        rb.velocity = new Vector2(_speed, rb.velocity.y);
    }
    protected void StartHunting()
    {
        //print("hunt" + " " + PlayerInAttackRange);
        if (PlayerInAttackRange || isCliff())
            return;
        animator.SetTrigger("Enemy_Move");
        chasing = true;
        float moveX = Destination(player.transform.position);
        moveHorizontal = moveX;
        Flip(player.transform.position);
        if (isCliff())
        {
            animator.SetTrigger("Enemy_Idle");
            moveX = 0;
        }
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        var d = player.transform.position - transform.position;
        mustGoHome = true;
    }
    protected void backToStartPos()
    {
        //print("home");
        if((int)Vector2.Distance(transform.position, startPos) <= 2)
        {
            mustGoHome = false;
            return;
        }
        chasing = false;
        float moveX = Destination(startPos);
        moveHorizontal = moveX;
        
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        Flip(startPos);
    }
    protected void Flip(Vector3 target)
    {
        Vector3 enemypos = transform.position;
        if ((int)target.x != (int)transform.position.x)
            if (enemypos.x < target.x && transform.localScale.x == -1 ||
                enemypos.x > target.x && transform.localScale.x == 1)
                transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    protected void Flip()
    {
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }
    protected void Patrol()
    {
        //print(isInWall);
        float moveX = transform.localScale.x;
        if ((isCliff() || isInWall) && !shouldTurn)
        {
            reachedPoint = true;
            if (moveX < 0)
            {
                spawnNewPoint(0, 5f);
                //print("spawn right");
                
            }
            else
            {
                spawnNewPoint(-5f, 0);
            }
            shouldTurn = true;
            reachedPoint = true;
            return;
        }
        if ((int)Vector2.Distance(transform.position, patrolPoint) <= 1)
        {
            reachedPoint = true;
            //patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
            //print("spawn plain");
            spawnNewPoint(-5, 5);
            return;
        }
        animator.SetTrigger("Enemy_Move");
        moveHorizontal = moveX;
        Move(speed * moveHorizontal);
        //Flip(patrolPoint);
        float PointX = patrolPoint.x;
        float PositionX = transform.position.x;
        moveX = transform.localScale.x;
        if (!(PositionX < PointX && moveX == 1 || PositionX > PointX && moveX == -1))
            Flip();
    }
    protected void spawnNewPoint(float a, float b)
    {
        patrolPoint = new Vector3((int)Random.Range(a, b) + startPos.x, startPos.y);
    }
    protected void checkGround()
    {
        Debug.DrawLine(groundPoint.position, groundPoint.position + new Vector3(0, -0.1f));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, 0.1f, ground);
        isGrounded = colliders.Length >= 1;
    }
    protected bool isCliff()
    {
        float moveX = transform.localScale.x;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position + new Vector3(moveX, 0), 0.1f, ground);
        //print(colliders.Length);
        return colliders.Length == 0;
        //isInfrontCliff = true;
    }
    public LayerMask getGround()
    {
        return ground;
    }
}
