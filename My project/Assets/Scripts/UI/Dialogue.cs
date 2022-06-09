using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    
    public int cost1 = 10;
    public int cost2 = 10;
    [SerializeField] private GameObject npc1;
    [SerializeField] private GameObject npc2;
    public GameObject responses;
    private GameObject btns;
    public bool startedConv = false;
    public GameObject player;
    public GameObject btn1;
    private bool leave = true;
    [SerializeField] Animator anim;
    private void Awake()
    {
        //btns = GameObject.Find()
        //player = GameObject.Find("Player");
    }
    private void Start()
    {
        gameObject.SetActive(false);
        print(cost1 + " " + cost2);
        //StartCoroutine(ShowDialog(test));
    }
    private void Update()
    {
        //if (npc1.GetComponent<npc>().playerNear && !startedConv)
        //    playDialog1();
    }
    public IEnumerator typeSentance(string str)
    {
        text.text = "";
        foreach(char letter in str.ToCharArray())
        {
            text.text += letter;
            yield return null;
        }
    }
    //private IEnumerator ShowDialog(DialogueObj obj)
    //{
    //    foreach(string str in obj.dialogue)
    //    {
    //        yield return StartCoroutine(typeSentance(str));
    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
    //    }
    //}
    private IEnumerator ShowDialog(string[] strings)
    {
        foreach (string str in strings)
        {
            //yield return StartCoroutine(typeSentance(str));
            text.text = str;
            yield return null;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        }
        responses.SetActive(true);
        print(leave);
        if (leave)
        {
            print("activate");
            btn1.SetActive(false);
        }
        else
        {
            btn1.SetActive(true);
        }
        StopAllCoroutines();
        //anim.SetBool("isClosed", true);
        //StartCoroutine(closeDialog());
    }
    private IEnumerator closeDialog()
    {
        anim.SetBool("isClosed", true);
        yield return new WaitForSeconds(0.5f);
    }
    public void playDialog1()
    {
        if (startedConv)
            return;
        leave = true;
        responses.SetActive(false);
        startedConv = true;
        string[] lines = new string[3];
        lines[0] = "���������� ���, ������ ������";
        lines[1] = "������ �������� ���� �� ��������?";
        if (player.GetComponentInChildren<CollectCoin>().Coins < cost1)
        {
            lines[2] = "� ��� ������������ ��������";
        }
        else
        {
            leave = false;
            lines[2] = "� ��� ������� ��������";
        }
        StopAllCoroutines();
        StartCoroutine(ShowDialog(lines));
    }
    public void playDialog2()
    {
        if (startedConv)
            return;
        leave = true;
        responses.SetActive(false);
        startedConv = true;
        string[] lines = new string[3];
        lines[0] = "�������, ������ ������";
        lines[1] = "����� �������� ���� ����?";
        if (player.GetComponentInChildren<CollectCoin>().Coins < cost2)
        {
            lines[2] = "������ � ���� �������� ��������";
        }
        else
        {
            leave = false;
            lines[2] = "� ���������� ��������";
        }
        btn1.GetComponentInChildren<Text>().text = "����� +1";
        StopAllCoroutines();
        StartCoroutine(ShowDialog(lines));
    }
}
