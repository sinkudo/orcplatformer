using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    SpriteRenderer sprite;
    private float dir;
    [SerializeField] private float speed = 5f;
    private bool stop = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            print("jump");
            rb.AddForce(Vector2.up * 10);
        }
    }
    private void FixedUpdate()
    {
        //print(dir);
        Move();
    }
    private void Move()
    {
        if (dir == -1f)
            sprite.flipX = true;
        else if (dir == 1f)
            sprite.flipX = false;
        rb.velocity = new Vector2(dir * speed * Time.deltaTime, rb.velocity.y);
    }
}
