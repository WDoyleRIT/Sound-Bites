using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    // private SceneManager sceneManager
    private SongSO currentSong;
    private uint levelScore;

    public UnityEvent OnSceneStart;
    public UnityEvent OnSceneStop;
}
