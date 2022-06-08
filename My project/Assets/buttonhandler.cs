using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonhandler : MonoBehaviour
{
    private GameObject p;
    private GameObject npc1;
    private GameObject npc2;
    [SerializeField] private Dialogue d;
    [SerializeField] private CollectCoin collectCoin;
    bool b;
    private void Awake()
    {
        p = GameObject.Find("Player");
        npc1 = GameObject.Find("NPC1");
        npc2 = GameObject.Find("NPC2");
    }
    //public void UpgradeHealth(int _cost, Health h)
    //{
    //    p.GetComponent<Health>().UpgradeHealth();
    //}
    public void Upgrade()
    {
        if (npc1.GetComponent<npc>().playerNear)
        {
            //Dialogue d = GameObject.Find("DialogueBox").GetComponent
            collectCoin.Coins -= d.cost1;
            d.cost1 *= 2;
            p.GetComponent<Health>().UpgradeHealth();
            close();
        }
        else if (npc2.GetComponent<npc>().playerNear)
        {
            collectCoin.Coins -= d.cost2;
            d.cost2 *= 2;
            p.GetComponent<Player>().UpgradeDamage();
            close();
        }
    }
    public void close()
    {
        GameObject.Find("DialogueBox").GetComponent<Dialogue>().startedConv = false;
        GameObject.Find("DialogueBox").SetActive(false);
    }
}
