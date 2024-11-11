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
    [SerializeField] public AudioSource songSource;
        
    [SerializeField] public FrequencyAveraging frequencyAveraging;
    [SerializeField] private FrequencyData frequencyData;

    public bool isTesting = false;

    public UnityEvent OnUpdate;

    public void OnSongStart()
    {
        // Set song volume based on settings (Does not work!)
        Debug.Log("Starting volume: " + songSource.volume);
        songSource.volume = GlobalVar.Instance.masterVol * GlobalVar.Instance.musicVol;
        Debug.Log("Ending volume: " + songSource.volume);

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
        songDelay = isTesting ? songDelay : GlobalVar.Instance.noteSpdInSec;
        GlobalVar.Instance.noteCoolDown = 60 / (float)currentSong.bpm * 4;

        switch (songDelay)
        {
            case 0:
                frequencySource.Play();
                songSource.Play();
                break;

            default:
                frequencySource.Play();

                yield return new WaitForSecondsRealtime(songDelay);

                songSource.Play();
                break;
        }
    }

    private IEnumerator StartData()
    {
        while (GlobalVar.Instance.songIsPlaying)
        {
            yield return new WaitForNextFrameUnit();

            OnUpdate.Invoke();
        }
    }

    private void ChangeSong(SongSO currentSong)
    {
        frequencyAveraging.ChangeSong(currentSong);
        songSource.clip = currentSong.audio;
    }
}
