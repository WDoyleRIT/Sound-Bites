using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrequencyAveraging : MonoBehaviour
{

    [SerializeField] private SongListSO SongListSO;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeSong<T>(T songID)
    {
        if (!(songID is string || songID is int)) throw new System.Exception("Value must be string(song name) or int(song ID)");

        SongSO song = SongListSO.GetSongSO(songID);
    }
}
