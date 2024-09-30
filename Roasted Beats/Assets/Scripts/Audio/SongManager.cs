using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FrequencyData), typeof(FrequencyAveraging))]
[RequireComponent(typeof(AudioSource))]
public class SongManager : MonoBehaviour
{
    [SerializeField] private float songDelay;
    [SerializeField] private SongSO currentSong;

    [SerializeField] private AudioSource frequencySource;
    [SerializeField] private AudioSource songSource;
}
