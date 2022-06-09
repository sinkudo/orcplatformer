using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallOut : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject DeathScreen;
    public Player player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            DeathScreen.SetActive(true);
            Time.timeScale = 0;
            //player.rb.velocity = Vector2.zero;
        }
    }
}
