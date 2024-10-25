using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private List<DialogueSO> dialogues;
    [SerializeField] private TextMeshPro TextMeshPro;
    [SerializeField] private GameObject Self;

    private string spokenText = "";
    private int currentDialogueIndex = 0;
    private int currentDialogueListIndex = 0;
    private DialogueSO currentDialogueList;

    private Coroutine currentActive;

    private IEnumerator DialogueLoop(string text, float timeInbetween)
    {
        spokenText = "";

        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            spokenText += text[i];

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }

        currentActive = null;
    }

    public void NextDialogueList()
    {
        currentDialogueListIndex++;
        currentDialogueList = dialogues[currentDialogueListIndex];
    }

    public void NextDialogue()
    {
        currentDialogueIndex++;

        DialogueObj temp = currentDialogueList.list[currentDialogueIndex];

        if (currentActive != null) StopCoroutine(currentActive);

        currentActive = StartCoroutine(DialogueLoop(temp.text, temp.charWaitTime));
    }

    public void SetActive(bool active)
    {
        Self.SetActive(active);
    } 
}
