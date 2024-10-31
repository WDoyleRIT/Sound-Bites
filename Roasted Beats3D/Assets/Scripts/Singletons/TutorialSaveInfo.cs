using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

#region Structs
[Serializable]
public struct TutSaveData
{
    public KeyValue<string, bool> SelectMenu;
    public KeyValue<string, bool> Cafe;
    public KeyValue<string, bool> Checkout;
    public KeyValue<string, bool> Cooking;
}

[Serializable]
public struct KeyValue<T, K>
{
    public T Key;
    public K Value;

    public KeyValue(T key, K value) 
    {
        Key = key;
        Value = value;
    }
}
#endregion

public class TutorialSaveInfo : Singleton<TutorialSaveInfo>
{
    private TextAsset tutorialSaveState;
    private Dictionary<string, bool> tutorialSaveInfo;

    private string dataDirPath;
    private string dataFileName = "TutorialSaveState";

    private Coroutine saveCoroutine;

    public Dictionary<string, bool> TutorialSaveState
    {
        get
        {
            if (tutorialSaveState == null)
            {
                LoadInfo();
            }

            return tutorialSaveInfo;
        }
    }

    //public delegate void Select(bool value);
    //public delegate void Cafe(bool value);
    //public delegate void Checkout(bool value);
    //public delegate void Cooking(bool value);

    //public event Select OnSelect;
    //public event Cafe OnCafe;
    //public event Checkout OnCheckout;
    //public event Cooking OnCooking;

    public void Awake()
    {
        dataDirPath = Application.persistentDataPath;

        LoadInfo();

        // Saving coroutine to be stopped later, as it will run until we stop it or program stops
        saveCoroutine = StartCoroutine(SaveHeartBeat());
    }

    public bool GetDictValue(string value)
    {
        return tutorialSaveInfo[value];
    }

    public void SetDictValue(string key, bool value)
    {
        tutorialSaveInfo[key] = value;
        SaveData();
    }

    // ==================================================================================================
    #region Load
    /// <summary>
    /// Loads data from persistent data path
    /// </summary>
    public void LoadInfo()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (!File.Exists(fullPath))
        {
            SaveBasicData();
        }

        string data = "";

        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
        }

        TutSaveData loadedData = JsonUtility.FromJson<TutSaveData>(data);

        UpdateDict(loadedData);
    }

    /// <summary>
    /// Use struct data to fill dictionary of values
    /// </summary>
    /// <param name="data"></param>
    private void UpdateDict(TutSaveData data)
    {
        tutorialSaveInfo = new Dictionary<string, bool>();

        tutorialSaveInfo.Add(data.SelectMenu.Key, data.SelectMenu.Value);
        tutorialSaveInfo.Add(data.Cafe.Key, data.Cafe.Value);
        tutorialSaveInfo.Add(data.Checkout.Key, data.Checkout.Value);
        tutorialSaveInfo.Add(data.Cooking.Key, data.Cooking.Value);
    }
    #endregion
    // ==================================================================================================

    // ==================================================================================================
    #region Save
    /// <summary>
    /// Saves data to persistent file path
    /// </summary>
    public void SaveData()
    {
        TutSaveData data = new TutSaveData();
        data.SelectMenu = new KeyValue<string, bool>("SelectMenu", tutorialSaveInfo["SelectMenu"]);
        data.Cafe = new KeyValue<string, bool>("Cafe", tutorialSaveInfo["Cafe"]);
        data.Checkout = new KeyValue<string, bool>("Checkout", tutorialSaveInfo["Checkout"]);
        data.Cooking = new KeyValue<string, bool>("Cooking", tutorialSaveInfo["Cooking"]);

        // https://youtu.be/aUi9aijvpgs

        string fullPath = Path.Combine(dataDirPath, dataFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }

    public void SaveData(TutSaveData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(data, true);

        Debug.Log(dataToStore);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(dataToStore);
            }
        }
    }

    public void SaveBasicData()
    {
        TutSaveData data = new TutSaveData();
        data.SelectMenu = new KeyValue<string, bool>("SelectMenu", false);
        data.Cafe = new KeyValue<string, bool>("Cafe", false);
        data.Checkout = new KeyValue<string, bool>("Checkout", false);
        data.Cooking = new KeyValue<string, bool>("Cooking", false);

        SaveData(data);
    }

    private IEnumerator SaveHeartBeat()
    {
        while (true)
        {
            SaveData();

            yield return new WaitForSeconds(10);
        }
    }
    #endregion
    // ==================================================================================================

}
