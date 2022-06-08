using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private DialogueObj test;
    //[SerializeField] private TextMesh text;
    private void Start()
    {
        
        StartCoroutine(ShowDialog(test));
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
    private IEnumerator ShowDialog(DialogueObj obj)
    {
        foreach(string str in obj.dialogue)
        {
            yield return StartCoroutine(typeSentance(str));
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.I));
        }
    }
}
