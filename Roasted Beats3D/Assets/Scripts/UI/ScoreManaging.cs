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
    [SerializeField] TextMeshPro rankPrefab;
    [SerializeField] TextMeshPro accuracyPrefab;
    private float accuracy;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        streak = 0;
        //textPrefab = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);

        // Hide the streak counter
        streakPrefab.text = "";

        rankPrefab.text = "Rank: S";
        accuracyPrefab.text = "Accuracy: 100%";
        accuracy = 0.00f;
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

        // If at least one note has passed, update the rank
        if(GlobalVar.Instance.notesPassed != 0)
        {
            ChangeRank();
        }
    }

    // Change rank based on accuracy
    // Known limitation of ranking system: Spamming the buttons when there is no note will note lower the accuracy.
    // The fix is probably just to increment the number of notes passed when this happens but this might interfere
    // With other people's code, so I left it as is for now.
    public void ChangeRank()
    {
        // Calculates note accuracy
        accuracy = GlobalVar.Instance.sumAccuracy / GlobalVar.Instance.notesPassed;

        accuracyPrefab.text = "Accuracy: " + accuracy.ToString("F2");

        // Accuracy thresholds are probably fine, but the actual distance thresholds for note accuracy need to be reevaluated,
        // As it is too difficult to consistently hit perfect notes.
        if(accuracy == 100.00f)
        {
            rankPrefab.text = "Rank: S";
        }
        else if(accuracy < 100.00f && accuracy >= 90.00f)
        {
            rankPrefab.text = "Rank: A";
        }
        else if (accuracy < 90.00f && accuracy >= 80.00f)
        {
            rankPrefab.text = "Rank: B";
        }
        else if (accuracy < 80.00f && accuracy >= 70.00f)
        {
            rankPrefab.text = "Rank: C";
        }
        else if (accuracy < 70.00f && accuracy >= 60.00f)
        {
            rankPrefab.text = "Rank: D";
        }
        else
        {
            rankPrefab.text = "Rank: F";
        }
    }

    public void BeatPressed(float points)
    {
        score += points;
    }
}
