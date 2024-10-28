using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private List<DialogueSO> dialogues;
    [SerializeField] private VoiceSO charVoice;

    [SerializeField] private List<AudioSource> audioSources;
    private int currentAudioSource;

    [SerializeField] private TextMeshPro TextMeshPro;
    [SerializeField] private GameObject Self;

    [SerializeField] private float defaultTextSpeed = .05f;

    [SerializeField] private float defaultPitch = 1;
    [SerializeField] private float defaultPitchOffset = .1f;

    private string spokenText = "";
    private int dialogueIndex = 0;
    private int currentDialogueListIndex = 0;
    private DialogueSO currentDialogueList;

    private Coroutine currentActive;

    private void Start()
    {
        currentDialogueList = dialogues[0];
    }

    private IEnumerator DialogueLoop(string text, float timeInbetween)
    {
        spokenText = "";

        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            spokenText += text[i];
            TextMeshPro.text = spokenText;

            Speak(charVoice.voiceList[Random.Range(0, charVoice.voiceList.Count)]);

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }

        currentActive = null;
    }

    private void Speak(AudioClip clip)
    {
        float pitch = defaultPitch + Random.Range(-defaultPitchOffset, defaultPitchOffset);

        audioSources[currentAudioSource].clip = clip;
        audioSources[currentAudioSource].pitch = pitch;
        audioSources[currentAudioSource].Play();

        currentAudioSource = (currentAudioSource + 1) % audioSources.Count;
    }

    public void NextDialogueList()
    {
        dialogueIndex = 0;

        currentDialogueListIndex++;

        currentDialogueListIndex = Mathf.Clamp(currentDialogueListIndex, 0, dialogues.Count - 1);

        currentDialogueList = dialogues[currentDialogueListIndex];
    }

    public void NextDialogue()
    {
        DialogueObj temp = currentDialogueList.dialogueList[dialogueIndex];

        if (currentActive != null) StopCoroutine(currentActive);

        float waitTime = (temp.charWaitTime == -1) ? defaultTextSpeed : temp.charWaitTime;

        currentActive = StartCoroutine(DialogueLoop(temp.text, waitTime));

        dialogueIndex++;
        dialogueIndex = Mathf.Clamp(dialogueIndex, 0, currentDialogueList.dialogueList.Count - 1);

        Debug.Log(dialogueIndex);
    }
    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("pressed left");
        //    NextDialogueList();
        //}
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
