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
    private bool justJumped = false;
    private bool holdingJump = false;
    Vector2 vmove;
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
        if (!justJumped && Input.GetButtonDown("Jump") && isGrounded)
        {
            justJumped = true;
            holdingJump = true;
        }
    }
    private void FixedUpdate()
    {
        checkGround();
        Move();
        Jump();
    }
    public  void Move()
    {
        if (moveHorizontal == -1f)
            sprite.flipX = true;
        else if (moveHorizontal == 1f)
            sprite.flipX = false;

        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }

    public bool jumpcontrol = false;
    public int jumpIter = 0;
    [SerializeField] public int jumpMax = 10;
    public void Jump()
    {
        if (justJumped)
        {

            if (Input.GetButton("Jump") && jumpIter++ < jumpMax)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            else
            {
                jumpIter = 0;
                justJumped = false;
            }
        }
    }

    void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
    }
}