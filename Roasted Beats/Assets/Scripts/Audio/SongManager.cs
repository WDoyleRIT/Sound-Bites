using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(FrequencyData), typeof(FrequencyAveraging))]
[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{
    [SerializeField] private float songDelay = 0;
    [SerializeField] private SongSO currentSong;

    [SerializeField] private AudioSource frequencySource;
    [SerializeField] private AudioSource songSource;
        
    [SerializeField] private FrequencyAveraging frequencyAveraging;
    [SerializeField] private FrequencyData frequencyData;

    public UnityEvent Update;

    public void OnSongStart()
    {
        StartCoroutine(StartMusic());
        StartCoroutine(StartData());
    }

    public void SetCurrentSong(SongSO currentSong)
    {
        this.currentSong = currentSong;

        ChangeSong(currentSong);
    }

    private IEnumerator StartMusic()
    {
        
        switch (songDelay)
        {
            case 0:
                frequencySource.Play();
                songSource.Play();
                break;

            default:
                frequencySource.Play();

                yield return new WaitForSeconds(songDelay);

                songSource.Play();
                break;
        }
    }

    private IEnumerator StartData()
    {
        while (GlobalVar.Instance.songIsPlaying)
        {
            yield return new WaitForNextFrameUnit();

            Update.Invoke();
        }
    }

    private void ChangeSong(SongSO currentSong)
    {
        frequencyAveraging.ChangeSong(currentSong);
        songSource.clip = currentSong.audio;
    }
}
