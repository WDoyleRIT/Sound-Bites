using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManaging : MonoBehaviour
{
    public void OpenLvl1(string name)
    {
        SceneManager.LoadScene("ForbesLvl1");
    }
}