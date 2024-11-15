using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : Singleton<SceneManaging>
{
    
    [SerializeField] private RectTransform transitionPanel;
    [SerializeField] private float waitTime = 0;

    // Can't Assign the text of button to this in the inspector
    [SerializeField] TextMeshProUGUI controlText;

    private Spring transitionSpring;
    private float offset;
    private AsyncOperation asyncLoad;
    private bool inSceneTransition;

    public Transform posStart;
    public Transform posIn;
    public Transform posOut;

    private float timer;
    public string currentLevel;

    public void OnContinue(SaveData saveData)
    {
        string sceneName = saveData.sceneName;

        OpenLvl(sceneName);
    }

    protected override void OnAwake()
    {
        transitionSpring = new Spring(10, 2f, offset = transitionPanel.position.x, false);

        GameSave.Instance.OnLoad += OnContinue;

        currentLevel = SceneManager.GetActiveScene().name;

        StartCoroutine(UpdateTransition());
    }

    private void Update()
    {
        
    }

    public void OpenLvl(string name)
    {
        currentLevel = name;

        StartCoroutine(LoadLevel(name));
    }
        
    IEnumerator LoadLevel(string scene)
    {
        // Play animation
        //transition.SetTrigger("TriggerSceneOut");

        transitionPanel.gameObject.SetActive(true);

        transitionSpring.RestPosition = posStart.position.x;
        transitionSpring.Position = posStart.position.x;

        transitionSpring.RestPosition = posIn.position.x;

        // Wait
        yield return new WaitForSeconds(waitTime);

        // LoadScene

        //UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

        asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            inSceneTransition = true;
            yield return null; // Wait for the next frame until the scene is fully loaded
        }

        transitionSpring.RestPosition = posOut.position.x;

        GameSave.Instance.SaveScene(scene);

        yield return new WaitForSeconds(.01f);
        //transition.SetTrigger("TriggerSceneIn");
        inSceneTransition = false;

        yield return new WaitForSeconds(1);

        transitionSpring.Position = posOut.position.x;

        timer = 1;

        transitionPanel.gameObject.SetActive(false);
    }

    private IEnumerator UpdateTransition()
    {
        while (true)
        {
            if (!inSceneTransition)
            {
                transitionSpring.Update();
            }

            timer -= Time.unscaledDeltaTime;

            if (timer < 0)
            {
                transitionPanel.position = posOut.position;
            }

            transitionPanel.position = new Vector3(transitionSpring.Position, transitionPanel.position.y, transitionPanel.position.z);

            yield return null;
        }
    }
}