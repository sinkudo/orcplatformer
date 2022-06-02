using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    int hp;
    int ragePool;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private bool isGrounded = false;
    float moveHorizontal;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;

    private bool clickedJump = false;
    private bool holdingJump = false;

    private bool facingRight = true;

    private bool canDash = true;
    private bool isDashing = false;
    [SerializeField] private float dashPower;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;

    private bool canAttack = true;
    private bool isAttacking = false;

    Vector2 vmove;
    private Player_State state
    {
        get { return (Player_State)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        vmove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveHorizontal = vmove.x;

        if (!clickedJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            clickedJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());
        if (isGrounded && moveHorizontal == 0 && !isAttacking) state = Player_State.Idle;
        //Attack();
    }
    private void FixedUpdate()
    {
        
        if (isDashing) return;
        
        if (moveHorizontal > 0 && !facingRight || moveHorizontal < 0 && facingRight) Flip();
        checkGround();
        Move();
        Jump();
        
    }
    public  void Move()
    {
        if (isGrounded && moveHorizontal != 0 && !isAttacking) state = Player_State.Run;
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    public int jumpIter = 0;
    [SerializeField] public int jumpMax = 10;
    public void Jump()
    {
        if (clickedJump)
        {
            state = Player_State.Jump;
            if (Input.GetButton("Jump") && jumpIter++ < jumpMax)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            else
            {
                jumpIter = 0;
                clickedJump = false;
            }
        }
    }
    void checkGround()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -0.1f));
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 thescale = transform.localScale;
        thescale.x *= -1;
        transform.localScale = thescale;
    }
    private IEnumerator Dash()
    {
        if (canDash)
        {
            state = Player_State.Dash;
            clickedJump = false;
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
            yield return new WaitForSeconds(dashTime);
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashCooldown);
            canDash = true;
        }
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            print("Attack");
            isAttacking = true;
            state = Player_State.Attack;
            //yield return new WaitForSeconds(1);
            //isAttacking = false;
        }
    }
}

public enum Player_State
{
    Idle,
    Run,
    Dash,
    Jump,
    Attack
}