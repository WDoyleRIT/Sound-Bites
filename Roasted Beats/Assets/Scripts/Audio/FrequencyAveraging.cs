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

    void Start()
    {
        ChangeSong<int>(0);
    }

    private void Update()
    {
        List<float> samples = FrequencyData.sampleData;
        List<bool> bools = new List<bool>();

        List<int> sampleRanges = currentSong.FreqMapDifficulties[GlobalVar.Instance.songDifficulty].SampleRanges;
        List<float> beatThreshholds = currentSong.FreqMapDifficulties[GlobalVar.Instance.songDifficulty].BeatThreshholds;

        for (int i = -1; i < sampleRanges.Count - 1; i++)
        {
            bools.Add(false);

            float avg = 0;

            for (int j = (i == -1 ? 0 : sampleRanges[i]); j < sampleRanges[i + 1]; j++ )
            {
                avg += samples[j];
            }

            avg /= sampleRanges[i + 1] - (i == -1 ? 0 : sampleRanges[i]);

            if (avg >= (!Testing ? beatThreshholds[i + 1] : beatSpawnThreshold)) bools[bools.Count - 1] = true; 
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
}