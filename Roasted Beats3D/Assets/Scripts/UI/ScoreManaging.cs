using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshPro textPrefab;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        textPrefab = Instantiate(textPrefab, textPrefab.transform.position, Quaternion.identity);

    }

    // Update is called once per frame
    void Update()
    {
        score = GlobalVar.Instance.currentLvlPoints;

        //textPrefab.text = score.ToString("Score: " + score);
        textPrefab.text = ("Score: " + score);
    }

    public void BeatPressed(float points)
    {
        score += points;
    }
}
