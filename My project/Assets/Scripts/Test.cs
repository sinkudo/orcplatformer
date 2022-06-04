using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Update()
    {
        Debug.DrawLine(transform.position, transform.position - Vector3.up);
    }
}
