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

    // Changes variable that determines the control scheme
    public void ChangeControls()
    {
        // Can't figure out how to change the text for some reason, will sort it out in class

        if(GlobalVar.Instance.controlScheme == 0)
        {
            GlobalVar.Instance.controlScheme = 1;
            //controlText.text = "Controls: A/S/D/F";
            Debug.Log("Controls: A/S/D/F");
        }
        else if (GlobalVar.Instance.controlScheme == 1)
        {
            GlobalVar.Instance.controlScheme = 0;
            //controlText.text = "Controls: D/F/J/K";
            Debug.Log("Controls: D/F/J/K");
        }
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