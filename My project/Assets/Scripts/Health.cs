using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float startingHealth;
    private Animator animator;
    public float curHealth { get; private set; }
    void Start()
    {
        animator = GetComponent<Animator>();
        curHealth = startingHealth;
    }
    public void TakeDamage(float _damage)
    {
        curHealth = Mathf.Clamp(curHealth - _damage, 0, startingHealth);
        if (curHealth <= 0)
        {
            animator.SetBool("Dead", true);
            if (GetComponent<Player>() != null)
                GetComponent<Player>().enabled = false;
            if (GetComponent<Enemy>() != null)
            {
                GetComponent<Enemy>().enabled = false;
                Destroy(gameObject);
            }
            //Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        print(curHealth);
        if (Input.GetKeyDown(KeyCode.F))
            TakeDamage(1);
    }
}
