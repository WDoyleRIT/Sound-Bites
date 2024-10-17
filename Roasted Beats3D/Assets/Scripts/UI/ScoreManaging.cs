using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private float score;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        //text = Instantiate(text, text.transform.position, text.transform.localRotation);

    }

    // Update is called once per frame
    void Update()
    {
        score = GlobalVar.Instance.currentLvlPoints;

        //textPrefab.text = score.ToString("Score: " + score);
        text.text = ("Score: " + score);
    }

    public void BeatPressed(float points)
    {
        score += points;
    }
}
