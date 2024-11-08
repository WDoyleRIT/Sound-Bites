using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
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
    [SerializeField] private TextMeshProUGUI TMPro;
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
    private bool isInDialogue;

    private List<Spring> charSprings;
    private List<Coroutine> charCoroutines;

    private Spring[] springs;

    private void Start()
    {
        charCoroutines = new List<Coroutine>();

        CreateSprings(20.0f,1f,0.0f,false);

        audioSources = sources.GetComponents<AudioSource>();

        currentDialogueList = dialogues[0];
        currentDialogueListIndex = 0;

        bool start = !TutorialSaveInfo.Instance.GetDictValue(tutInfo[currentDialogueListIndex]);

        StartCoroutine(SetActive(start));

        if (start)
        {
            NextDialogue();
        }
    }

    private void CreateSprings(float v1, float v2, float v3, bool v4)
    {
        springs = new Spring[2];

        springs[0] = new Spring(v1, v2, v3, v4);
        springs[1] = new Spring(v1, v2, v3, v4);
    }

    private IEnumerator DialogueLoop(string text, float timeInbetween)
    {
        spokenText = "";
        TMPro.text = spokenText;   

        SpawnCharacter();

        isInDialogue = true;

        if (currentSpeechActive != null) StopCoroutine(currentSpeechActive);

        currentSpeechActive = StartCoroutine(SpeechLoop(timeInbetween));

        int textLength = text.Length;

        for (int i = 0; i < textLength; i++)
        {
            spokenText += text[i];
            TMPro.text = spokenText;
            //charCoroutines.Add(StartCoroutine(SpawnTextChar(i))); 

            float newTime = timeInbetween + Random.Range(-timeInbetween / 4, timeInbetween / 4);

            yield return new WaitForSecondsRealtime(newTime);
        }

        isInDialogue = false;

        StopCoroutine(currentSpeechActive);
    }

    /// <summary>
    /// Deprecated/Not-Working
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    private IEnumerator SpawnTextChar(int i)
    {
        TMPro.ForceMeshUpdate();

        float time = 2;

        TMP_CharacterInfo TMPC = TMPro.textInfo.characterInfo[i];

        charSprings.Add(new Spring(5, .25f, 0, false));
        charSprings[i].Position = 10;

        Vector3[] vertices = TMPro.mesh.vertices;

        int charIndex = TMPC.vertexIndex; 

        while (time > 0)
        {
            Vector3 offset = Vector3.up * charSprings[i].Position;
            
            vertices[charIndex + 0] += offset;
            vertices[charIndex + 1] += offset;
            vertices[charIndex + 2] += offset;
            vertices[charIndex + 3] += offset;

            Vector3[] tempVertices = TMPro.mesh.vertices;

            tempVertices[charIndex + 0] = vertices[charIndex + 0];
            tempVertices[charIndex + 1] = vertices[charIndex + 1];
            tempVertices[charIndex + 2] = vertices[charIndex + 2];
            tempVertices[charIndex + 3] = vertices[charIndex + 3];

            TMPro.mesh.SetVertices(tempVertices);

            time -= Time.unscaledDeltaTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }
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
        for (int i = 0; i < charCoroutines.Count; i++)
            StopCoroutine(charCoroutines[i]);

        charCoroutines.Clear();
        charSprings = new List<Spring>();

        DialogueObj temp = currentDialogueList.dialogueList[dialogueIndex];

        if (currentDialogueActive != null) StopCoroutine(currentDialogueActive);

        float waitTime = (temp.charWaitTime == -1) ? defaultTextSpeed : temp.charWaitTime;

        currentDialogueActive = StartCoroutine(DialogueLoop(temp.text, waitTime));

        dialogueIndex++;

        Debug.Log(dialogueIndex);
    }
    private void Update()
    {
        Self.transform.localScale = new Vector3(1/*springs[0].Position*/, Mathf.Abs(springs[1].Position));

        foreach (Spring s in springs)
            s.Update();

        //foreach (Spring s in charSprings) 
        //    s.Update();

        if ((Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space)) && isActive)
        {
            if (currentDialogueActive != null && isInDialogue)
            {
                SkipDialogue();
                return;
            }

            if (currentDialogueList.dialogueList.Count == dialogueIndex)
            {
                StopAllCoroutines();
                StartCoroutine(SetActive(false));
                return;
            }

            NextDialogue();
        }
    }

    private void SkipDialogue()
    {
        StopCoroutine(currentDialogueActive);
        currentDialogueActive = null;

        DialogueObj temp = currentDialogueList.dialogueList[dialogueIndex - 1];

        spokenText = temp.text;
        TMPro.text = spokenText;

        StopCoroutine(currentSpeechActive);

        isInDialogue = false;
    }

    public IEnumerator SetActive(bool active)
    {
        AudioListener.pause = active;

        foreach (AudioSource source in audioSources)
        {
            source.ignoreListenerPause = true;
        }

        if (!active)
        {
            springs[0].RestPosition = 0.0f;
            springs[1].RestPosition = 0.0f;

            Time.timeScale = 1.0f;

            yield return new WaitForSeconds(.5f);
        }

        Self.SetActive(active);
        isActive = active;

        if (active)
        {
            yield return new WaitForSeconds(.1f);

            springs[0].RestPosition = 1.0f;
            springs[1].RestPosition = 1.0f;

            Time.timeScale = 0.0f;
        }


        if (!active)
        {
            TutorialSaveInfo.Instance.SetDictValue(tutInfo[currentDialogueListIndex], true);
        }
    }
}
