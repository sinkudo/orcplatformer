using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    public float curHealth { get; private set; }
    void Start()
    {
        curHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        //curHealth = Mathf.Clamp(curHealth - _damage, 0, startingHealth);
        curHealth -= _damage;
        //if (curHealth <= 0)
        //    Destroy(gameObject);
        //print(curHealth);
    }
    // Update is called once per frame
    void Update()
    {
        print(curHealth);
        if (Input.GetKeyDown(KeyCode.F))
            TakeDamage(1);
    }
}
