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
    private float currentDelay = 0f;
    [SerializeField] GameObject Attack_Hitbox;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask ground;
    protected bool isGrounded = true;
    protected Transform groundPoint;

    protected override void Attack()
    {
        
    }
    //private bool PlayerInAttackRange()
    //{
    //    RaycastHit2D hit = Physics2D.BoxCast(transform.position + new Vector3(1.25f, -0.47f), new Vector3(1, 0.5f), 0, Vector2.left, 0, playerLayer);
    //    return hit.collider != null;
    //}
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
    }
    void Update()
    {
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
            StartHunting();
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
        yield return new WaitForSeconds(0.65f);
        //Attack_Hitbox.SetActive(false);
        animator.SetTrigger("Enemy_Idle");
        isAttacking = false;
        justAttacked = true;
        PlayerInAttackRange = false;
        //print(justAttacked);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(patrolPoint, 0.3f);
    }
    public void Move(float _speed)
    {
        rb.velocity = new Vector2(_speed, rb.velocity.y);
    }
    protected void StartHunting()
    {
        //print("hunt");
        if (PlayerInAttackRange)
            return;
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
        chasing = false;
        float moveX = Destination(startPos);
        moveHorizontal = moveX;
        
        rb.velocity = new Vector2(speed * moveX, rb.velocity.y);
        Flip(startPos);
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
        if ((int)Vector2.Distance(transform.position, patrolPoint) <= 1)
        {
            reachedPoint = true;
            patrolPoint = new Vector3((int)Random.Range(-5f, 5f) + startPos.x, startPos.y);
            return;
        }
        animator.SetTrigger("Enemy_Move");
        float moveX = Destination(patrolPoint);
        moveHorizontal = moveX;
        Move(speed * moveHorizontal);
        Flip(patrolPoint);
        //Flip();
    }
    //protected void DealDamage()
    //{
    //    player.TakeDamage(damage);
    //}
    void checkGround()
    {
        Debug.DrawLine(groundPoint.position, groundPoint.position + new Vector3(0, -0.1f));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundPoint.position, 0.1f, ground);
        isGrounded = colliders.Length >= 1;
    }
}
