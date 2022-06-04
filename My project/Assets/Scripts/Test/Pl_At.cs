using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl_At : MonoBehaviour
{
    GameObject aa = default;
    private bool attacking = false;
    float ttk = 0.25f;
    float timer = 0f;
    private void Start()
    {
        aa = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        if (attacking)
        {
            timer += Time.deltaTime;
            if (timer >= ttk)
            {
                timer = 0;
                attacking = false;
                aa.SetActive(attacking);
            }
        }

    }
    private void Attack()
    {
        attacking = true;
        aa.SetActive(attacking);
    }
}
