using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinSceneManager : MonoBehaviour
{

    [SerializeField] TextMeshPro highScoreText;

    public Button ReturnToSelect;

    private bool loaded = false;

    // Start is called before the first frame update
    private void Start()
    {
        if (!loaded)
        {
            for(int i = 0; i < 5; i++)
            {
                string value = "Score"+(i+1);
                GlobalVar.Instance.scores[i]=HighScoreSaveInfo.Instance.GetDictValue(value);
            }
        }


        bool newScorePlaced = false;
        int goodScoreHold = 0;
        int badScoreHold = 0;

        for (int i = 0; i < 5; i++)
        {
            string value = "Score" + (i + 1);
            if (GlobalVar.Instance.currentLvlPoints > GlobalVar.Instance.scores[i] && !newScorePlaced)
            {
                newScorePlaced = true;
                goodScoreHold = GlobalVar.Instance.scores[i];
                GlobalVar.Instance.scores[i] = GlobalVar.Instance.currentLvlPoints;
            }
            else if (newScorePlaced)
            {
                badScoreHold = GlobalVar.Instance.scores[i];
                GlobalVar.Instance.scores[i] = goodScoreHold;
                goodScoreHold = badScoreHold;
            }
            HighScoreSaveInfo.Instance.SetDictvalue(value, GlobalVar.Instance.scores[i]);
        }

        highScoreText.text = "High Score: " + GlobalVar.Instance.scores[0]
            + "\n2nd: " + GlobalVar.Instance.scores[1]
            + "\n3rd: " + GlobalVar.Instance.scores[2]
            + "\n4th: " + GlobalVar.Instance.scores[3]
            + "\n5th: " + GlobalVar.Instance.scores[4];

        ReturnToSelect.onClick.RemoveAllListeners();
        ReturnToSelect.onClick.AddListener(() => SceneManaging.Instance.OpenLvl("RestaurantSelect"));
    }

    // Update is called once per frame
    private void Update()
    {
        /*
        if (GlobalVar.Instance.currentLvlPoints > GlobalVar.Instance.highScore) {

            for(int i = 4; i >= 1; i--)
            {
                GlobalVar.Instance.scores[i] = GlobalVar.Instance.scores[i - 1];
            }


            GlobalVar.Instance.highScore = GlobalVar.Instance.currentLvlPoints;
            GlobalVar.Instance.scores[0] = GlobalVar.Instance.currentLvlPoints;

        }
        */



    }
}
