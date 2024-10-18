using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class RhythmManager : MonoBehaviour
{
    // private SceneManager sceneManager
    [SerializeField] private SongListSO songList;
    private int currentSongIndex;
    private int levelScore;

    [SerializeField] private SongManager sm;
    [SerializeField] private AudioMixerGroup groupAudio;

    public List<int> songIndicesForThisLevel;
    public UnityEvent OnSceneStart;
    public UnityEvent OnSceneStop;

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

    private void Update()
    {
        GlobalVar.Instance.currentLvlPoints = levelScore;
    }
}
