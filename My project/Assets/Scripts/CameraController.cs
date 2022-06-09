using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float camSpeed = 2f;
    [SerializeField] private Transform target;
    // Start is called before the first frame update
    private void Awake()
    {
        if (!target) target = FindObjectOfType<Player>().transform;
    }
    void Start()
    {
        if (!target) target = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position;
        pos.z = -10f;
        pos.y += 2f;
        transform.position = Vector3.Lerp(transform.position, pos, camSpeed * Time.deltaTime);
    }
}
