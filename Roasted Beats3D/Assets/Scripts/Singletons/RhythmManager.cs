using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using TMPro;

public class RhythmManager : MonoBehaviour
{
    // private SceneManager sceneManager
    [SerializeField] private SongListSO songList;
    private int currentSongIndex;
    private int levelScore;
    private int levelStreak;

    [SerializeField] private SongManager sm;
    [SerializeField] private AudioMixerGroup groupAudio;

    public List<int> songIndicesForThisLevel;
    public UnityEvent OnSceneStart;
    public UnityEvent OnSceneStop;

    // Text Field for Rating box
    [SerializeField] TextMeshPro ratingText;

    private void Start()
    {
        OnStart();
    }

    public void OnStart()
    {
        int songIndex = songIndicesForThisLevel[Random.Range(0, songIndicesForThisLevel.Count)];

        sm.SetCurrentSong(songList.GetSongSO<int>(songIndex));

        GlobalVar.Instance.songIsPlaying = true;

        OnSceneStart.Invoke();

        // Dropping variable to wake up playerInput;
        //var input = InputManager.Instance.playerInput;

        GameManager.Instance.CurrentLevel = this;

        // Hide rating text
        ratingText.text = "";
    }

    public void SetVolume(float volume)
    {
        //
    }

    public void ChangeScoreBy(int score)
    {
        levelScore += score;
        levelScore = Mathf.Max(levelScore, 0);
    }

    // Simple method to increase the streak when a note is hit,
    // and to reset the streak when a note is missed.
    public void ChangeStreak(int streak)
    {
        if (streak == 0)
        {
            levelStreak = 0;
        }
        else
        {
            levelStreak++;
        }
    }

    // Changes the text that tells the player how accurate their note press is
    public void ChangeRating(int rating)
    {
        switch (rating)
        {
            case 0:
                ratingText.text = "Miss";
                break;
            case 1:
                ratingText.text = "OK";
                break;
            case 2:
                ratingText.text = "Good";
                break;
            case 3:
                ratingText.text = "Great!";
                break;
            case 4:
                ratingText.text = "Perfect!!";
                break;
        }
    }

    private void Update()
    {
        GlobalVar.Instance.currentLvlPoints = levelScore;
        GlobalVar.Instance.streak = levelStreak;
    }
}
