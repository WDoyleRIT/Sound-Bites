using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    // private SceneManager sceneManager
    [SerializeField] private SongListSO songList;
    private int currentSongIndex;
    private uint levelScore;

    [SerializeField] private SongManager sm;

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
    }
}
