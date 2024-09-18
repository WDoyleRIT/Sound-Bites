using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

/// <summary>
///  Uses Fast Fourier Transform (FFT) to get sample data from audio source
/// </summary>
[ExecuteAlways]
public class FrequencyData : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    public List<float> sampleData;
    private float[] frequencies;

    public int sampleLength; 

    // Update is called once per frame
    void Update()
    {
        float[] samples = new float[sampleLength]; // = new float[audio.clip.samples * audio.clip.channels];

        if (Application.isPlaying) audio.GetSpectrumData(samples, 0, FFTWindow.Hanning);
        
        sampleData = samples.ToList();
    }

    /// <summary>
    /// Update audio source and FFT with Song Obj
    /// </summary>
    /// <param name="song"></param>
    public void UpdateSong(SongSO song)
    {
        audio.clip = song.audio;
        sampleLength = song.sampleSize;
    }
}
