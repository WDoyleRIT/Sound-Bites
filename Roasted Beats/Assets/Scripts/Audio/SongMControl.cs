using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongMControl : MonoBehaviour
{
    public bool start;
    public SongManager SongManager;
    public int songIndex;
    public SongListSO songList;

    private void Start()
    {
        SongManager.SetCurrentSong(songList.GetSongSO<int>(songIndex));

        GlobalVar.Instance.songIsPlaying = true;
    }

    private void Update()
    {
        if (start)
        {
            SongManager.OnSongStart();
            start = false;
        }
    }
}
