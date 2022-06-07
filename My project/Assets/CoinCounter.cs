using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinCounter : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    CollectCoin collect;
    TextMeshProUGUI textmesh;
    void Start()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
        collect = GameObject.Find("Player").GetComponentInChildren<CollectCoin>();
        textmesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //textmesh.text = player.Coins.ToString();
        textmesh.text = collect.Coins.ToString();
    }
}
