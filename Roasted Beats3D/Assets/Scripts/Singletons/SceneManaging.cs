using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : Singleton<SceneManaging>
{
    
    [SerializeField] private Animator transition;
    [SerializeField] private float waitTime = 0;

    // Can't Assign the text of button to this in the inspector
    [SerializeField] TextMeshPro controlText;

    public void OpenLvl(string name)
    {
        StartCoroutine(LoadLevel(name));
    }
        
    IEnumerator LoadLevel(string scene)
    {
        // Play animation
        //transition.SetTrigger("TriggerSceneOut");

        // Wait
        yield return new WaitForSeconds(waitTime);

        // LoadScene

        //UnityEngine.SceneManagement.SceneManager.LoadScene(scene);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        while (!asyncLoad.isDone)
        {
            yield return null; // Wait for the next frame until the scene is fully loaded
        }

        //transition.SetTrigger("TriggerSceneIn");

    }
    
}