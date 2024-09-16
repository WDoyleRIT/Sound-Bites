using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(FrequencyData))]
public class FrequencyGraph : MonoBehaviour
{
    [SerializeField] private FrequencyData frequencyData;

    [SerializeField] private GameObject frequencyBar;

    [SerializeField][Range(0,1)] private float barSpacing = .25f;
    [SerializeField][Range(0, 100)] private float barScale = 50;
    [SerializeField][Range(0, 1)] private float barWidth = .005f;
    [SerializeField][Range(0, .1f)] private float barMin = .05f;

    [SerializeField] private float halves = 8;

    private List<GameObject> bars;

    private List<float> samples;
    private int sampleLength;

    private void Start()
    {
        sampleLength = frequencyData.sampleLength;

        if (bars != null)
        {
            for (int i = 0; i < bars.Count; i++)
            {
                if (!Application.isPlaying) GameObject.DestroyImmediate(bars[i]);
                else GameObject.Destroy(bars[i]);
            }
        }

        bars = new List<GameObject>();

        for (int i = 0; i < sampleLength / halves; i++)
        {
            bars.Add(Instantiate(frequencyBar, new Vector3(transform.position.x + i * barSpacing - (sampleLength / (halves * 2) * barSpacing), transform.position.y), Quaternion.identity, transform));
        }
    }

    void Update()
    {
        if (sampleLength != frequencyData.sampleLength)
        {
            for (int i = 0; i < bars.Count; i++)
            {
                if (!Application.isPlaying) GameObject.DestroyImmediate(bars[i]);
                else GameObject.Destroy(bars[i]);
            }

            bars = new List<GameObject>();

            for (int i = 0; i < sampleLength / halves; i++)
            {
                bars.Add(Instantiate(frequencyBar, new Vector3(transform.position.x + i * barSpacing - (sampleLength / (halves * 2) * barSpacing), transform.position.y), Quaternion.identity, transform));
            }

            sampleLength = frequencyData.sampleLength;
        }

        samples = frequencyData.sampleData;

        for (int i = 0; i < samples.Count / halves; i++)
        {
            GameObject bar = bars[i];

            bar.transform.position = new Vector3(transform.position.x + i * barSpacing - (samples.Count / (halves * 2) * barSpacing), transform.position.y);

            Vector3 scale = bar.transform.localScale;

            scale.x = barWidth;
            scale.y = Mathf.Max(samples[i] * barScale, barMin);

            bar.transform.localScale = scale;
        }
    }
}
