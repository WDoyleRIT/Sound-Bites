using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FrequencyInEditor : MonoBehaviour
{
    [SerializeField] private FrequencyData frequencyData;
    [SerializeField] private FrequencyGraph frequencyGraph;

    [SerializeField][Range(0, 1)] private float barSpacing = .025f;
    [SerializeField][Range(0, 1000)] private float barScale = 50;
    [SerializeField][Range(0, 1)] private float barWidth = .005f;
    [SerializeField][Range(0, .1f)] private float barMin = .01f;

    [SerializeField] private GameObject frequencyBar;

    [SerializeField] private bool ChangeSampleSize;
    [SerializeField] private bool GenerateBars;

    [SerializeField] private float halves = 16;

    public List<GameObject> Bars;
    public float BarMin { get { return barMin; } }
    public float BarScale { get { return barScale; } }

    private void Update()
    {
        int sampleLength = frequencyData.sampleLength;

        if (ChangeSampleSize)
        {
            ChangeSampleSize = false;

            if (Bars != null)
            {
                for (int i = 0; i < Bars.Count; i++)
                {
                    GameObject.DestroyImmediate(Bars[i]);
                }
            }
        }

        if (GenerateBars)
        {
            if (Bars != null)
            {
                for (int i = 0; i < Bars.Count; i++)
                {
                    GameObject.DestroyImmediate(Bars[i]);
                }
            }

            GenerateBars = false;

            Bars = new List<GameObject>();

            for (int i = 0; i < sampleLength / halves; i++)
            {
                Bars.Add(Instantiate(frequencyBar, new Vector3(transform.position.x + i * barSpacing - (sampleLength / (halves * 2) * barSpacing), transform.position.y), Quaternion.identity, transform));
                //bars[i].transform.position = new Vector3(transform.position.x + i * barSpacing - (samples.Count / (halves * 2) * barSpacing), transform.position.y);

                Vector3 scale = Bars[Bars.Count - 1].transform.localScale;

                scale.x = barWidth;

                Bars[Bars.Count - 1].transform.localScale = scale;
                Bars[Bars.Count - 1].transform.name = string.Format("Frequency Bar #{0:0000}", Bars.Count);
            }
        }

        if (Application.isPlaying) return;

        if (Bars == null) return;

        for (int i = 0; i < Bars.Count; i++)
        {
            GameObject bar = Bars[i];

            Vector3 scale = bar.transform.localScale;

            Bars[i].transform.position = new Vector3(transform.position.x + i * barSpacing - (sampleLength / (halves * 2) * barSpacing), transform.position.y);

            scale.y = Mathf.Max(0, BarMin);
            scale.x = barWidth;

            bar.transform.localScale = scale;
        }
    }
}
