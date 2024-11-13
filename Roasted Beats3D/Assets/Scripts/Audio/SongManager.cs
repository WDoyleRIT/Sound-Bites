using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

[RequireComponent(typeof(FrequencyData), typeof(FrequencyAveraging))]
[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{
    [SerializeField] private float songDelay = 0;
    [SerializeField] public SongSO currentSong;

    [SerializeField] private AudioSource frequencySource;
    [SerializeField] public AudioSource songSource;
        
    [SerializeField] public FrequencyAveraging frequencyAveraging;
    [SerializeField] private FrequencyData frequencyData;

    // Reference to the audio mixer
    [SerializeField] private AudioMixer mixer;
    // Float for the music volume
    private float volume = 0;

    public bool isTesting = false;

    public UnityEvent OnUpdate;

    public void OnSongStart()
    {
        // Set song volume based on settings (Range from -60db to 0db)
        volume = (GlobalVar.Instance.masterVol * GlobalVar.Instance.musicVol * 60.0f) - 60.0f;
        Debug.Log("Music Volume: " + volume);
        mixer.SetFloat("volume", volume);

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

        float time = GlobalVar.Instance.saveData.songData.timeOfSong;

        if (time > 0)
        {
            frequencySource.time = time - songDelay;
            songSource.time = time;

            frequencySource.Play();
            songSource.Play();
        }
        else
        {
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
