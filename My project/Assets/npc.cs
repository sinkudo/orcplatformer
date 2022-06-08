using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    GameObject voskl;
    GameObject dialog;
    public bool playerNear = false;
    private void Awake()
    {
        voskl = transform.Find("Dialogue icon").gameObject;
        dialog = GameObject.Find("DialogueBox");
    }
    private void Start()
    {
        voskl.SetActive(false);
    }
    private void Update()
    {
        //if(voskl.activeSelf && Input.GetKeyDown(KeyCode.I))
        //{
        //    playerNear = true;
        //    dialog.GetComponent<Dialogue>().gameObject.SetActive(true);
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerNear = true;
            voskl.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            voskl.SetActive(false);
            playerNear = false;
        }
    }
}
