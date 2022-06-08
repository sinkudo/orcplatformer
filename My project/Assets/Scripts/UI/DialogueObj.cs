using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Dialogue obj")]
public class DialogueObj : ScriptableObject
{
    [SerializeField] public string[] dialogue;
}
