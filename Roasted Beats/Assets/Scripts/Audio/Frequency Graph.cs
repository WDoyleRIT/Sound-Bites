using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Graphs frequencies from Frequency Data
/// </summary>
[ExecuteAlways]
[RequireComponent(typeof(FrequencyData), typeof(AudioSource), typeof(FrequencyInEditor))]
public class FrequencyGraph : MonoBehaviour
{
    [SerializeField] private FrequencyData frequencyData;
    [SerializeField] private FrequencyInEditor frequencyInEditor;

    private List<float> samples;
    private int sampleLength;

    public List<GameObject> bars;

    private void Start()
    {
        sampleLength = frequencyData.sampleLength;
    }

    void Update()
    {
        sampleLength = frequencyData.sampleLength;
        samples = frequencyData.sampleData;

        bars = frequencyInEditor.Bars;

        // Scales frequency bars based on sample frequencies level
        for (int i = 0; i < bars.Count; i++)
        {
            GameObject bar = frequencyInEditor.Bars[i];

            Vector3 scale = bar.transform.localScale;

            scale.y = Mathf.Max(samples[i] * frequencyInEditor.BarScale, frequencyInEditor.BarMin);

            bar.transform.localScale = scale;
        }
    }
}
