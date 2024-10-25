using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
        //currentDialogueList = dialogues[currentDialogueListIndex];
        //currentDialogueListIndex++;
        //currentDialogueListIndex = Mathf.Clamp(currentDialogueListIndex, 0, dialogues.Count - 1);
    }

    public void NextDialogue()
    {
        //DialogueObj temp = currentDialogueList.list[currentDialogueIndex];

        //if (currentActive != null) StopCoroutine(currentActive);

        //currentActive = StartCoroutine(DialogueLoop(temp.text, temp.charWaitTime));

        //currentDialogueIndex++;
        //currentDialogueIndex = Mathf.Clamp(currentDialogueIndex, 0, dialogues[currentDialogueListIndex - 1].list.Count - 1);

        //Debug.Log(currentDialogueIndex);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("pressed left");
            NextDialogueList();
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("pressed right");
            NextDialogue();
        }
    }

    public void SetActive(bool active)
    {
        Self.SetActive(active);
    } 
}
