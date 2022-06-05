using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private Rigidbody2D rb;
    Vector2 vmove;
    float moveHorizontal;
    [SerializeField]
    float speed = 4f;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //sprite = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponent<Animator>();
    }
    private void Update()
    {
        vmove = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveHorizontal = vmove.x;
        
    }
    private void FixedUpdate()
    {
        if (moveHorizontal < 0 || moveHorizontal > 0)
            Move();
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
    }
}
