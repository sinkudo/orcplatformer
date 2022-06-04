using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA : MonoBehaviour
{
    int damage = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Hl>())
        {
            Hl h = collision.GetComponent<Hl>();
            h.Damage(damage);
        }
    }
}
