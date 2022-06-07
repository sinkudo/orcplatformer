using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Player : MonoBehaviour
{
    //[SerializeField] private float hp;
    private float ragePool;
    private float damage = 33f;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    private bool isGrounded = false;
    float moveHorizontal;
    public Rigidbody2D rb { get; private set; }
    public SpriteRenderer sprite { get; private set; }
    private Animator animator;
    [SerializeField] public LayerMask ground;
    [SerializeField] private GameObject Attack_Hitbox;
    [SerializeField] private float Damage;
    private Health health;

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
    private float timeForCombo = 0f;
    private int combocnt = 0;
    //[SerializeField] private Tilemap tilemap;
    [SerializeField] private GridLayout grid;
    //[SerializeField] Tile tile;
    public bool isInvincible { get; private set; }

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
        Attack_Hitbox.SetActive(false);
        //isInvincible = false;
        isInvincible = true;
        //tilemap = GameObject.Find("Grid").GetComponentInChildren<Tilemap>();
        //grid = GameObject.Find("Grid").GetComponent<Grid>();
    }

    void Update()
    {
        if (!isAttacking && combocnt > 0)
            timeForCombo += Time.deltaTime;
        if (timeForCombo >= 1f)
        {
            timeForCombo = 0f;
            combocnt = 0;
        }
        if (combocnt >= 3)
        {
            timeForCombo = 0;
            combocnt = 0;
        }
        vmove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveHorizontal = vmove.x;

        if (!clickedJump && Input.GetButtonDown("Jump") && isGrounded)
            clickedJump = true;

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            StartCoroutine(Dash());

        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            if (combocnt == 0)
                state = Player_State.Attack1;
            else if (combocnt == 1)
                state = Player_State.Attack2;
            else if (combocnt == 2)
                state = Player_State.Attack;
            combocnt++;
            StartCoroutine(DoAttack());
        }
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
    public void Move()
    {
        if (isGrounded && !isAttacking) state = Player_State.Run;

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }
    public int jumpIter = 0;
    [SerializeField] public int jumpMax = 10;
    public void Jump()
    {
        if (!isGrounded && !isAttacking)
            state = Player_State.Jump;
        if (!isGrounded && isAttacking)
        {
            rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
            clickedJump = false;
        }
        else if (clickedJump)
        {
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, ground);
        isGrounded = colliders.Length >= 1;
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
            isInvincible = true;
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
            isInvincible = false;
        }
    }
    private IEnumerator Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isAttacking)
        {
            isAttacking = true;
            state = Player_State.Attack;
            Attack_Hitbox.SetActive(true);

            yield return new WaitForSeconds(0.3f);
            Attack_Hitbox.SetActive(false);
            isAttacking = false;
        }
    }

    public float getPlayerDamage()
    {
        return Damage;
    }
    IEnumerator DoAttack()
    {
        //
        //Вызов ActivateHitBox() происходит по ивенту во время выполнения анимации
        //
        yield return new WaitForSeconds(0.3f);
        Attack_Hitbox.transform.localScale += new Vector3(-0.0001f, 0, 0);
        Attack_Hitbox.SetActive(false);
        isAttacking = false;
    }
    public bool getPlayerDirection()
    {
        return facingRight;
    }
    private void ActivateHitbox()
    {
        Attack_Hitbox.SetActive(true);
        Attack_Hitbox.transform.localScale += new Vector3(0.0001f, 0, 0);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
    public IEnumerator blink()
    {
        Material matDefault = sprite.material;
        Material matBlink = Resources.Load("Flash") as Material;
        sprite.material = matBlink;
        yield return new WaitForSeconds(0.1f);
        sprite.material = matDefault;
    }
}

public enum Player_State
{
    Idle,
    Run,
    Dash,
    Jump,
    Attack,
    Attack1,
    Attack2
}