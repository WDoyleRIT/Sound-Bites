using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DialogueSO : ScriptableObject
{
     public List<DialogueObj> dialogueList;
}

[Serializable]
public class DialogueObj
{
    public Sprite sprite;
    public string text;
    public float charWaitTime;
}
