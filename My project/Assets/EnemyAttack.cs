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
        if (collision.gameObject.name == "Player")
            enem.PlayerInAttackRange = true;
    }
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.name == "Player")
    //        enem.PlayerInAttackRange = false;
    //}
}
