using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

#region Structs

[Serializable]
public struct SaveData
{
    public string sceneName;
    public CustomerData[] customers;
    public SongData songData;
    public int score;
}

[Serializable]
public struct CustomerData
{
    public string name;
    public Vector3 position;
    public int[] orderData;

    public CustomerData(string name, Vector3 position, int[] orderData)
    {
        this.name = name;
        this.position = position;
        this.orderData = orderData;
    }
}

[Serializable]
public struct SongData
{
    public float timeOfSong;
    public string songName;
    public NoteData[] notes;

    public SongData(float timeOfSong, NoteData[] notes, string songName)
    {
        this.timeOfSong = timeOfSong;
        this.notes = notes;
        this.songName = songName;
    }
}

[Serializable]
public struct NoteData
{
    public int barNumber;
    public Vector3 position;
    public float speed;

    public NoteData(int barNumber, Vector3 position, float speed)
    {
        this.barNumber = barNumber;
        this.position = position;
        this.speed = speed;
    }
}
#endregion

public class GameSave : Singleton<GameSave>
{
    private SaveData saveData;
    private string fullPath;

    public delegate void LoadData(SaveData saveData);
    public event LoadData OnLoad;

   protected override void OnAwake()
   {
        fullPath = Path.Combine(Application.persistentDataPath, "SaveData");

        saveData = new SaveData();

        if (!DataIO.Instance.CheckFilePath(fullPath))
            DataIO.Instance.SavaData<SaveData>(saveData, fullPath);

        saveData = DataIO.Instance.LoadData<SaveData>(fullPath);

        StartCoroutine(SaveHeartBeat());
    }

    public void Continue()
    {
        if (saveData.sceneName == null || saveData.sceneName == "")
            return;

        OnLoad.Invoke(saveData);
        GlobalVar.Instance.saveData = saveData;
    }

    public void SaveSong(SongData songData)
    {
        saveData.songData = songData;
    }

    public void SaveScene(string sceneName)
    {
        saveData.sceneName = sceneName;
    }

    public void SaveCustomers(CustomerData[] customers)
    {
        saveData.customers = customers;
    }

    public void SaveScore(int currentScore)
    {
        saveData.score = currentScore;
    }

    public IEnumerator SaveHeartBeat()
    {
        while (true)
        {
            DataIO.Instance.SavaData<SaveData>(saveData, fullPath);

            yield return new WaitForSecondsRealtime(5);
        }
    }
}
