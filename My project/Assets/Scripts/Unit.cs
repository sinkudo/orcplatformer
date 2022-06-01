using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    int hp;
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    private bool isGrounded = false;
    float moveHorizontal;
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Animator animator;
    public abstract void Move();
    public abstract void Jump();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
        
}
