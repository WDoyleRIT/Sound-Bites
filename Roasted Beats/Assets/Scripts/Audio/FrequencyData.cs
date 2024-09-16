using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

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
}
