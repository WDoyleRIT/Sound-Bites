using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshPro textPrefab;
    [SerializeField] TextMeshPro streakPrefab;
    private float score;
    private int streak;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        streak = 0;
        //textPrefab = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);

        // Hide the streak counter
        streakPrefab.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        score = GlobalVar.Instance.currentLvlPoints;
        streak = GlobalVar.Instance.streak;

        //textPrefab.text = score.ToString("Score: " + score);
        textPrefab.text = ("Score: " + score);

        // Hides the streak indicator if there is none
        if (score < 0)
        {
            streakPrefab.text = "";
        }
        // Otherwise, display the streak counter
        else
        {
            streakPrefab.text = ("x" + streak);
            // Wildly inefficient solution to the streak
            // sometimes displaying despite the value being 0
            // Will reassess in the near future - Will
            if (streakPrefab.text == "x0")
            {
                streakPrefab.text = "";
            }
        }
    }

    public void BeatPressed(float points)
    {
        score += points;
    }
}
