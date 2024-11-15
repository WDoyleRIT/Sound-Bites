using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[Serializable]

public struct ScoreSaveData
{
    public KeyValue<string, int> score1;
    public KeyValue<string, int> score2;
    public KeyValue<string, int> score3;
    public KeyValue<string, int> score4;
    public KeyValue<string, int> score5;
}


public class HighScoreSaveInfo : Singleton<HighScoreSaveInfo>
{
    private TextAsset scoreSaveState;
    private Dictionary<string, int> scoreSaveInfo;

    private string dataDirPath;
    private string dataFileName = "ScoreSaveState";

    public Dictionary<string, int> ScoreSaveState
    {
        get
        {
            if(scoreSaveState == null)
            {
                LoadInfo();
            }

            return scoreSaveInfo;
        }
    }


    public void Awake()
    {
        dataDirPath = Application.persistentDataPath;

        LoadInfo();


    }

    public int GetDictValue(string value)
    {
        return scoreSaveInfo[value];
    }

    public void SetDictvalue(string key, int value)
    {
        scoreSaveInfo[key] = value;
        SaveData();
    }


    public void LoadInfo()
    {
        string fullPath=Path.Combine(dataDirPath, dataFileName);

        if(!DataIO.Instance.CheckFilePath(fullPath))
        {
            SaveBasicData();
        }

        ScoreSaveData loadedData=DataIO.Instance.LoadData<ScoreSaveData>(fullPath);

        UpdateDict(loadedData);
    }


    private void UpdateDict(ScoreSaveData data)
    {
        scoreSaveInfo = new Dictionary<string, int>();

        scoreSaveInfo.Add(data.score1.Key, data.score1.Value);
        scoreSaveInfo.Add(data.score2.Key, data.score2.Value);
        scoreSaveInfo.Add(data.score3.Key, data.score3.Value);
        scoreSaveInfo.Add(data.score4.Key, data.score4.Value);
        scoreSaveInfo.Add(data.score5.Key, data.score5.Value);
    }


    public void SaveData()
    {
        ScoreSaveData data= new ScoreSaveData();
        data.score1 = new KeyValue<string, int>("Score1", scoreSaveInfo["Score1"]);
        data.score2 = new KeyValue<string, int>("Score2", scoreSaveInfo["Score2"]);
        data.score3 = new KeyValue<string, int>("Score3", scoreSaveInfo["Score3"]);
        data.score4 = new KeyValue<string, int>("Score4", scoreSaveInfo["Score4"]);
        data.score5 = new KeyValue<string, int>("Score5", scoreSaveInfo["Score5"]);

        string fullPath = Path.Combine(dataDirPath, dataFileName);

        DataIO.Instance.SavaData<ScoreSaveData>(data,fullPath);
    }

    public void SaveData(ScoreSaveData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        DataIO.Instance.SavaData<ScoreSaveData>(data, fullPath);
    }



    public void SaveBasicData()
    {
        ScoreSaveData data=new ScoreSaveData();
        data.score1 = new KeyValue<string, int>("Score1", 0);
        data.score2 = new KeyValue<string, int>("Score2", 0);
        data.score3 = new KeyValue<string, int>("Score3", 0);
        data.score4 = new KeyValue<string, int>("Score4", 0);
        data.score5 = new KeyValue<string, int>("Score5", 0);

        SaveData(data);
    }
}
