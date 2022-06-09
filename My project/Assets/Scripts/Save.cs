using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public GameObject Player;
    [SerializeField] private Dialogue d;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("save") == 1 && PlayerPrefs.GetInt("load") == 1)
        {
            float px = PlayerPrefs.GetFloat("Player_x");
            float py = PlayerPrefs.GetFloat("Player_y");
            print(px + " " + py);
            Player.transform.position = new Vector2(px, py);
            Player.GetComponent<Health>().loadHealth(PlayerPrefs.GetFloat("maxHp"), PlayerPrefs.GetFloat("hp"));
            Player.GetComponent<Player>().LoadDamage(PlayerPrefs.GetFloat("damage"));
            Player.GetComponentInChildren<CollectCoin>().LoadCoin(PlayerPrefs.GetInt("coins"));
            d.cost1 = PlayerPrefs.GetInt("cost1");
            d.cost2 = PlayerPrefs.GetInt("cost2");
        }
        Time.timeScale = 1f;
    }
    public void SaveGame()
    {
        print("saved");
        PlayerPrefs.SetFloat("hp", Player.GetComponent<Health>().curHealth);
        PlayerPrefs.SetFloat("maxHp", Player.GetComponent<Health>().startingHealth);
        PlayerPrefs.SetFloat("damage", Player.GetComponent<Player>().getPlayerDamage());
        PlayerPrefs.SetFloat("Player_x", Player.transform.position.x);
        PlayerPrefs.SetFloat("Player_y", Player.transform.position.y);
        PlayerPrefs.SetInt("coins", Player.GetComponentInChildren<CollectCoin>().Coins);
        PlayerPrefs.SetInt("save", 1);
        PlayerPrefs.SetInt("cost1", d.cost1);
        PlayerPrefs.SetInt("cost2", d.cost2);
        PlayerPrefs.Save();
    }
    public void LoadGame()
    {
        PlayerPrefs.SetInt("load", 1);
        PlayerPrefs.Save();
    }
}
