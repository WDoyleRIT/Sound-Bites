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

    public void OnContinue(SaveData saveData)
    {
        string sceneName = saveData.sceneName;

        OpenLvl(sceneName);
    }

    protected override void OnAwake()
    {
        transitionSpring = new Spring(10, 2f, offset = transitionPanel.position.x, false);

        GameSave.Instance.OnLoad += OnContinue;
    }

    private void Update()
    {
        if (!inSceneTransition)
        {
            transitionSpring.Update();
        }

        transitionPanel.position = new Vector3(transitionSpring.Position, transitionPanel.position.y, transitionPanel.position.z);
    }

    public void OpenLvl(string name)
    {
        StartCoroutine(LoadLevel(name));
    }
        
    IEnumerator LoadLevel(string scene)
    {
        

        // Play animation
        //transition.SetTrigger("TriggerSceneOut");

        transitionSpring.RestPosition = offset;
        transitionSpring.Position = offset;

        transitionSpring.RestPosition = 1000;

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

        transitionSpring.RestPosition = -offset;

        GameSave.Instance.SaveScene(scene);

        yield return new WaitForSeconds(.05f);
        //transition.SetTrigger("TriggerSceneIn");
        inSceneTransition = false;
    }
    
}