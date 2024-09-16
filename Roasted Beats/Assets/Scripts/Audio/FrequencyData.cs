using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FrequencyData : MonoBehaviour
{
    [SerializeField] private AudioSource audio;

    public List<float> sampleData;
    private float[] frequencies;

    public int sampleLength; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float[] samples = new float[sampleLength]; // = new float[audio.clip.samples * audio.clip.channels];

        audio.GetSpectrumData(samples, 0, FFTWindow.Rectangular);
        sampleData = samples.ToList();
    }
}
