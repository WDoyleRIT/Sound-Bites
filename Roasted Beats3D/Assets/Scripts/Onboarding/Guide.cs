using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private List<DialogueSO> dialogues;
    [SerializeField] private VoiceSO charVoice;

    [Header("Audio")]
    [SerializeField] private GameObject sources;
    private AudioSource[] audioSources;
    private int currentAudioSource;

    [SerializeField] private float defaultPitch = 1;
    [SerializeField] private float defaultPitchOffset = .1f;


    [Header("Text")]
    [SerializeField] private TextMeshProUGUI TextMeshPro;
    [SerializeField] private float defaultTextSpeed = .05f;

    [Header("Other")]
    [SerializeField] private GameObject Self;
    [SerializeField] private RectTransform charSpawnPoint;

    [SerializeField] private List<string> tutInfo;

    private string spokenText = "";
    private int dialogueIndex = 0;
    private int currentDialogueListIndex = 0;
    private DialogueSO currentDialogueList;

    private Coroutine currentDialogueActive;
    private Coroutine currentSpeechActive;

    private Image charSprite;
    private bool isActive;

    private void Start()
    {
        audioSources = sources.GetComponents<AudioSource>();

        currentDialogueList = dialogues[0];
        currentDialogueListIndex = 0;

        bool start = !TutorialSaveInfo.Instance.GetDictValue(tutInfo[currentDialogueListIndex]);

        SetActive(start);

        if (start)
        {
            NextDialogue();
        }
    }

    private IEnumerator DialogueLoop(string text, float timeInbetween)
    {
        spokenText = "";

        SpawnCharacter();

        if (currentSpeechActive != null) StopCoroutine(currentSpeechActive);

        currentSpeechActive = StartCoroutine(SpeechLoop(timeInbetween));

        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            spokenText += text[i];
            TextMeshPro.text = spokenText;

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }

        StopCoroutine(currentSpeechActive);
    }

    private void SpawnCharacter()
    {
        if (charSprite != null) GameObject.Destroy(charSprite.gameObject);

        charSprite = new GameObject("CharacterGuide", typeof(Image), typeof(RectTransform)).GetComponent<Image>();
        charSprite.sprite = currentDialogueList.dialogueList[dialogueIndex].sprite;
        charSprite.preserveAspect = true;
        charSprite.transform.localScale = Vector3.one * 6;
        charSprite.transform.SetParent(charSpawnPoint.transform, false);
    }

    private IEnumerator SpeechLoop(float timeInbetween)
    {
        timeInbetween *= 3;

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

        currentAudioSource = (currentAudioSource + 1) % audioSources.Length;
    }

    /// <summary>
    /// Sets next dialogue list
    /// </summary>
    public void SetDialogueList(int value)
    {
        dialogueIndex = 0;

        currentDialogueListIndex = value;

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

        Debug.Log(dialogueIndex);
    }
    private void Update()
    {
        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isActive)
        {
            if (currentDialogueList.dialogueList.Count == dialogueIndex)
            {
                StopAllCoroutines();
                SetActive(false);
                return;
            }

            NextDialogue();
        }
    }

    public void SetActive(bool active)
    {
        Time.timeScale = active ? 0.0f : 1.0f;
        AudioListener.pause = active;

        foreach (AudioSource source in audioSources)
        {
            source.ignoreListenerPause = true;
        }

        Self.SetActive(active);
        isActive = active;

        if (!active)
            TutorialSaveInfo.Instance.SetDictValue(tutInfo[currentDialogueListIndex], true);
    }
}
