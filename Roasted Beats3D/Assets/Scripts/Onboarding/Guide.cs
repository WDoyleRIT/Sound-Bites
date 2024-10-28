using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private List<DialogueSO> dialogues;
    [SerializeField] private VoiceSO charVoice;

    [Header("Audio")]
    [SerializeField] private List<AudioSource> audioSources;
    private int currentAudioSource;

    [SerializeField] private float defaultPitch = 1;
    [SerializeField] private float defaultPitchOffset = .1f;


    [Header("Text")]
    [SerializeField] private TextMeshPro TextMeshPro;
    [SerializeField] private float defaultTextSpeed = .05f;

    [Header("Other")]
    [SerializeField] private GameObject Self;
    [SerializeField] private Transform charSpawnPoint;

    private string spokenText = "";
    private int dialogueIndex = 0;
    private int currentDialogueListIndex = 0;
    private DialogueSO currentDialogueList;

    private Coroutine currentDialogueActive;
    private Coroutine currentVoiceActive;

    private void Start()
    {
        currentDialogueList = dialogues[0];
    }

    private IEnumerator DialogueLoop(string text, float timeInbetween)
    {
        spokenText = "";

        if (currentVoiceActive != null) currentVoiceActive = null;

        currentVoiceActive = StartCoroutine(SpeechLoop(timeInbetween));

        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            spokenText += text[i];
            TextMeshPro.text = spokenText;

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }

        currentVoiceActive = null;
        currentDialogueActive = null;
    }

    private IEnumerator SpeechLoop(float timeInbetween)
    {
        timeInbetween *= 2;

        while (true)
        {
            Speak(charVoice.voiceList[Random.Range(0, charVoice.voiceList.Count)]);

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }
    }

    private void Speak(AudioClip clip)
    {
        float pitch = defaultPitch + Random.Range(-defaultPitchOffset, defaultPitchOffset);

        audioSources[currentAudioSource].clip = clip;
        audioSources[currentAudioSource].pitch = pitch;
        audioSources[currentAudioSource].Play();

        currentAudioSource = (currentAudioSource + 1) % audioSources.Count;
    }

    /// <summary>
    /// Sets next dialogue list
    /// </summary>
    public void NextDialogueList()
    {
        dialogueIndex = 0;

        currentDialogueListIndex++;

        currentDialogueListIndex = Mathf.Clamp(currentDialogueListIndex, 0, dialogues.Count - 1);

        currentDialogueList = dialogues[currentDialogueListIndex];
    }

    /// <summary>
    /// Starts next string of text in dialogue list
    /// </summary>
    public void NextDialogue()
    {
        DialogueObj temp = currentDialogueList.dialogueList[dialogueIndex];

        if (currentDialogueActive != null) StopCoroutine(currentDialogueActive);

        float waitTime = (temp.charWaitTime == -1) ? defaultTextSpeed : temp.charWaitTime;

        currentDialogueActive = StartCoroutine(DialogueLoop(temp.text, waitTime));

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
