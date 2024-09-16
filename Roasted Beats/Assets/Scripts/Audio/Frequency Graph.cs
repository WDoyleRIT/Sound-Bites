using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class FrequencyGraph : MonoBehaviour
{
    [SerializeField] private FrequencyData frequencyData;

    [SerializeField] private GameObject frequencyBar;

    [SerializeField] private float barSpacing;
    [SerializeField] private float barScale;
    [SerializeField] private float barWidth;

    private List<GameObject> bars;

    private List<float> samples;

    private void Start()
    {
        bars = new List<GameObject>();

        for (int i = 0; i < frequencyData.sampleLength; i++)
        {
            bars.Add(Instantiate(frequencyBar, new Vector3(transform.position.x + i * barSpacing, transform.position.y), Quaternion.identity, transform));
        }
    }

    void Update()
    {
        samples = frequencyData.sampleData;

        for (int i = 0; i < bars.Count; i++)
        {
            GameObject bar = bars[i];
            Vector3 scale = bar.transform.localScale;

            scale.x = barWidth;
            scale.y = samples[i] * barScale;

            bar.transform.localScale = scale;
        }
    }
}
