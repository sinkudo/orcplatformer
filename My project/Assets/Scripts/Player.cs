using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private float hp;
    private float ragePool;
    private float damage = 33f;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private bool isGrounded = false;
    float moveHorizontal;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    public LayerMask enemyLayers;
    [SerializeField] private GameObject attackHitbox;

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
    private float Damage = 33f;

    //Player_Attack hitbox;

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
        attackHitbox.SetActive(false);
        //hitbox = attackHitbox.GetComponent<Player_Attack>();
    }

    void Update()
    {
        //print(hitbox.s);
        vmove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveHorizontal = vmove.x;

        if (!clickedJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            clickedJump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());
        StartCoroutine(Attack());
    }
    private void FixedUpdate()
    {
        
        if (isDashing) return;
        if (moveHorizontal > 0 && !facingRight || moveHorizontal < 0 && facingRight) Flip();

        checkGround();
        if (moveHorizontal < 0 || moveHorizontal > 0)
            Move();
        else
            Stay();
        Jump();
    }
    private void Stay()
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        if (isGrounded && !isAttacking)
            state = Player_State.Idle;
    }
    public  void Move()
    {
        //if (isGrounded && moveHorizontal != 0 && !isAttacking) state = Player_State.Run;
        if(isGrounded && !isAttacking) state = Player_State.Run;
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    public int jumpIter = 0;
    [SerializeField] public int jumpMax = 10;
    public void Jump()
    {
        if (!isGrounded && !isAttacking)
            state = Player_State.Jump;
        if (clickedJump)
        {
            //state = Player_State.Jump;
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
    private IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            state = Player_State.Attack;
            attackHitbox.SetActive(true);
            
            yield return new WaitForSeconds(0.3f);
            attackHitbox.SetActive(false);
            isAttacking = false;
        }
        
    }
    public float getPlayerDamage()
    {
        return Damage;
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