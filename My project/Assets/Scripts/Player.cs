using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int hp;
    int mp;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] private bool isGrounded = false;
    float moveHorizontal;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool justJumped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (!justJumped && Input.GetButtonDown("Jump") && isGrounded)
            justJumped = true;
    }
    private void FixedUpdate()
    {
        checkGround();
        if (moveHorizontal < -0.1f || moveHorizontal > 0.1f)
            Run();
        if (justJumped)
            Jump();
        
    }
    void Run()
    {
        sprite.flipX = moveHorizontal < 0;
        rb.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
    }
    void Jump()
    {
        justJumped = false;
        rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }
    void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
    }
}
