using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FrequencyAveraging : MonoBehaviour
{
    /// <summary>
    /// Observer pattern to let song depedent scripts know of song change
    /// </summary>
    [SerializeField] private UnityEvent<SongSO> OnChangeSong;

    [SerializeField] private SongListSO SongListSO;

    void Start()
    {
        ChangeSong<int>(0);
    }

    /// <summary>
    /// Will get song obj from an ID and let any script relying on song changes know the new song
    /// </summary>
    /// <typeparam name="T">Int or string</typeparam>
    /// <param name="songID"></param>
    /// <exception cref="System.Exception"></exception>
    public void ChangeSong<T>(T songID)
    {
        if (!(songID is string || songID is int)) throw new System.Exception("Value must be string(song name) or int(song ID)");

        SongSO song = SongListSO.GetSongSO(songID);

        OnChangeSong.Invoke(song);
    }
}
