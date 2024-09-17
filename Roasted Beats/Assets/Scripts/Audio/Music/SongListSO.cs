using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class SongListSO : ScriptableObject
{
    [SerializeField] private List<SongSO> songSOs;

    private Dictionary<string, SongSO> songSODict;

    /// <summary>
    /// Creates song dictionary based on song name for faster finding
    /// </summary>
    private void Awake()
    {
        foreach (SongSO song in songSOs)
        {
            songSODict.Add(song.Name, song);
        }
    }

    /// <summary>
    /// Return song scriptable object from dictionary
    /// </summary>
    /// <param name="songName"></param>
    /// <returns></returns>
    public SongSO GetSongSO(string songName)
    {
        return songSODict[songName];
    }

    /// <summary>
    /// Return song scriptable object from list
    /// </summary>
    /// <param name="songID"></param>
    /// <returns></returns>
    public SongSO GetSongSO(int songID)
    {
        return songSOs[songID];
    }
}

/// <summary>
/// SO object for easy frequency mapping for a song, see more ->
/// </summary>
// Ie, Ethernight might have a thumpier bass and requires a smaller baseline frequency sample from 0-100
// and the higher frequencies might be 4000-8192, Pixel Polka might have a bass mapping of 0-400 bc it doesn't have as thumpy of a bass
[Serializable]
public class SongSO
{
    [SerializeField] public AudioClip audio;
    [SerializeField] public String Name;
    [SerializeField] public Vector2Int FrequencyMapping1;
    [SerializeField] public Vector2Int FrequencyMapping2;
    [SerializeField] public Vector2Int FrequencyMapping3;
    [SerializeField] public Vector2Int FrequencyMapping4;
    [SerializeField] public Vector2Int FrequencyMapping5;
}
