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
    private void Awake()
    {
        collect = GameObject.Find("Player").GetComponentInChildren<CollectCoin>();
        textmesh = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        ////player = GameObject.Find("Player").GetComponent<Player>();
        //collect = GameObject.Find("Player").GetComponentInChildren<CollectCoin>();
        //textmesh = GetComponent<TextMeshProUGUI>();
        textmesh.text = collect.Coins.ToString();
        print(collect.Coins);
    }

    // Update is called once per frame
    void Update()
    {
        textmesh.text = collect.Coins.ToString();
        //print("Coin Counter " +collect.Coins);
    }
}
