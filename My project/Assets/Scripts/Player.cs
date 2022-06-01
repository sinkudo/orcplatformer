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
        //if (moveHorizontal < -0.1f || moveHorizontal > 0.1f)
        //    Move();
        Move();
        Jump();
        //if (justJumped)
        //{
        //    if (Input.GetButton("Jump"))
        //        holdingJump = true;
        //    else
        //        holdingJump = false;
        //    Jump();
        //}
    }
    public  void Move()
    {
        //print(transform.position);
        //if(moveHorizontal == 0f)
        //{
        //    rb.velocity = Vector2.zero;
        //    return;
        //}
        if (moveHorizontal == -1f)
            sprite.flipX = true;
        else if (moveHorizontal == 1f)
            sprite.flipX = false;

        //rb.AddForce(new Vector2(moveHorizontal * speed, 0f), ForceMode2D.Impulse);
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);

        //if (moveHorizontal == -1f)
        //    sprite.flipX = true;
        //else if (moveHorizontal == 1f)
        //    sprite.flipX = false;
        //rb.velocity = new Vector2(moveHorizontal * speed * Time.deltaTime, rb.velocity.y);
    }

    //public bool jumpcontrol = false;
    //public int jumpIter = 20;
    //public int jumpValueIter = 60;
    //public void Jump()
    //{
    //    print(jumpIter + " " + holdingJump);
    //    if (jumpIter == 20)
    //    {
    //        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //        jumpIter++;
    //    }
    //    else if(jumpIter <= 30 && holdingJump)
    //    {

    //        rb.AddForce(Vector2.up * (jumpForce + jumpIter));
    //        jumpIter++;
    //    }
    //    else
    //    {
    //        justJumped = false;
    //        jumpIter = 20;
    //    }
    //}

    public bool jumpcontrol = false;
    public int jumpIter = 0;
    public int jumpMax = 10;
    //public void Jump()
    //{
    //    print(jumpIter + " " + holdingJump + " " + jumpMax);
    //    if (jumpIter == 0 && holdingJump)
    //    {
    //        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    //        jumpIter++;
    //    }
    //    else if (jumpIter <= jumpMax && holdingJump)
    //    {
    //        rb.AddForce(Vector2.up * jumpForce);
    //        jumpIter++;
    //    }
    //    else
    //    {
    //        justJumped = false;
    //        jumpIter = 0;
    //    }
    //}

    public void Jump()
    {
        if (justJumped)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            justJumped = false;
        }
    }

    void checkGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        isGrounded = colliders.Length > 1;
    }
}
