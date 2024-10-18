using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : Singleton<SceneManaging>
{
    
    [SerializeField] private Animator transition;
    [SerializeField] private float waitTime = 0;
    
    public void OpenLvl(string name)
    {
        StartCoroutine(LoadLevel(name));
    }
        
    IEnumerator LoadLevel(string scene)
    {
        // Play animation
        //transition.SetTrigger("start");

        // Wait
        yield return new WaitForSeconds(waitTime);

        // LoadScene
        //UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
        SceneManager.LoadScene(scene);
    }
    
}