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
    [SerializeField] private UnityEvent<List<bool>> OnSpawnBeat;

    [SerializeField] private SongListSO SongListSO;
    [SerializeField] private FrequencyData FrequencyData;

    [SerializeField][Range(0, .005f)] private float beatSpawnThreshold;

    [SerializeField] private bool Testing;

    private SongSO currentSong;

    public void UpdateData()
    {
        List<float> samples = FrequencyData.sampleData;
        List<bool> bools = new List<bool>();

        List<Vector2Int> sampleRanges = currentSong.FreqMapDifficulties[GlobalVar.Instance.songDifficulty].SampleRanges;
        List<float> beatThreshholds = currentSong.FreqMapDifficulties[GlobalVar.Instance.songDifficulty].BeatThreshholds;

        for (int i = 0; i < sampleRanges.Count; i++)
        {
            bools.Add(false);

            float avg = 0;

            for (int j = sampleRanges[i].x; j < sampleRanges[i].y; j++ )
            {
                avg += samples[j];
            }

            avg /= sampleRanges[i].y - sampleRanges[i].x;

            if (avg >= (!Testing ? beatThreshholds[i] : beatSpawnThreshold)) bools[bools.Count - 1] = true; 
        }

        OnSpawnBeat.Invoke(bools);
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

        currentSong = SongListSO.GetSongSO(songID);

        OnChangeSong.Invoke(currentSong);
    }

    public void ChangeSong(SongSO song)
    {
        currentSong = song;
        OnChangeSong.Invoke(currentSong);
    }
}
