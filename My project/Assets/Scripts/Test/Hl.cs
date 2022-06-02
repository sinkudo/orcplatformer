using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hl : MonoBehaviour
{
    [SerializeField]int health = 9;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(int amount)
    {
        print("damage");
        this.health -= amount;
        if (health <= 0)
            Die();
    }
    void Die()
    {
        print("dead");
        Destroy(gameObject);
    }
}
