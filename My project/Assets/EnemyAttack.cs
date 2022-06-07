using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private MovingEnemy enem;
    private void Start()
    {
        enem = GetComponentInParent<MovingEnemy>();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //    {
    //        enem.isAttacking = true;
    //    }
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        //print(collision.gameObject.layer);
        if (collision.gameObject.name == "Player")
        {
            enem.canDamage = true;
            enem.PlayerInAttackRange = true;
        }
        if (collision.gameObject.layer == 3)
            enem.isInWall = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
            enem.canDamage = false;
        if (collision.gameObject.layer == enem.getGround())
            enem.isInWall = false;
    }
}
