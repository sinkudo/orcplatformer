using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackScript : MonoBehaviour
{

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        print(collision.gameObject.name);
    }
}
