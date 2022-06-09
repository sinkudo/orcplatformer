using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MovingEnemy
{
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
        PlayerHealth = GameObject.Find("Player").GetComponent<Health>();
    }
    private void finishedAttack()
    {
        animator.SetBool("finishedAttack", true);
        
        animator.Play("Boss_Idle");
        justAttacked = true;
        animator.SetTrigger("Enemy_Idle");
        print("finished");
    }
    private void StartedAttack()
    {
        animator.SetBool("finishedAttack", false);
    }
    private void Update()
    {
        print(justAttacked);
        checkGround();
        if (!isGrounded)
            return;
        if (PlayerInAttackRange && !justAttacked)
        {
            //StartCoroutine(doAttack());
            print(PlayerInAttackRange);
            doAttack();
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
        {
            if (isCliff())
                return;
            StartHunting();
        }
        else if (mustGoHome)
            backToStartPos();
        else Patrol();
    }
    new protected void doAttack()
    {
        //print("attack");
        animator.Play("Boss_Attack");
        //animator.SetTrigger("Enemy_Attack");
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        //yield return new WaitForSeconds(attackAnimationTime);
        //animator.SetTrigger("Enemy_Idle");
        isAttacking = false;
        justAttacked = true;
        PlayerInAttackRange = false;
    }
}
