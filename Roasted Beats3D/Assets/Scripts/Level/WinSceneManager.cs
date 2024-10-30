using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinSceneManager : MonoBehaviour
{

    [SerializeField] TextMeshPro highScoreText;

    // Start is called before the first frame update
    private void Start()
    {
        highScoreText.text = "High Score: " + GlobalVar.Instance.currentLvlPoints;
    }

    // Update is called once per frame
    private void Update()
    {
        if (GlobalVar.Instance.currentLvlPoints > GlobalVar.Instance.highScore) {
            GlobalVar.Instance.highScore = GlobalVar.Instance.currentLvlPoints;
        }
    }
}
