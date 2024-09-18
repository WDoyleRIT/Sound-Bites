using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

 /// <summary>
 /// Something just to store integers without using array that uses VectorInt naem bc I think it looks nice
 /// </summary>
[Serializable]
public struct VectorInt
{
    public List<int> Ints;
    public void Add(int value)
    {
        Ints.Add(value);
    }
}

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
    /// Return song scriptable object from list
    /// </summary>
    /// <param name="songID"></param>
    /// <returns></returns>
    public SongSO GetSongSO<T>(T song)
    {
        if (song is string) return songSODict[song.ToString()];
        if (song is int) return songSOs[Convert.ToInt32(song)];

        throw new Exception("Song value was not int or string");
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
    [SerializeField] public int sampleSize; // must be multiple of 64 between 64-8192
    [SerializeField] public VectorInt FreqMapEasy;
    [SerializeField] public VectorInt FreqMapMedium;
    [SerializeField] public VectorInt FreqMapHard;
}
