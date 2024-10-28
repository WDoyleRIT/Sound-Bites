using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public struct TutSaveData
{
    public KeyValuePair<string, bool> SelectMenu;
    public KeyValuePair<string, bool> Cafe;
    public KeyValuePair<string, bool> Checkout;
    public KeyValuePair<string, bool> Cooking;
}

public class TutorialSaveInfo : Singleton<TutorialSaveInfo>
{
    private TextAsset tutorialSaveState;
    private Dictionary<string, bool> tutorialSaveInfo;

    private string dataDirPath;
    private string dataFileName = "TutorialSaveState";

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

    /// <summary>
    /// Loads data from persistent data path
    /// </summary>
    public void LoadInfo()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (!File.Exists(fullPath)) return;

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
    /// Saves data to persistent file path
    /// </summary>
    public void SaveData()
    {
        TutSaveData data = new TutSaveData();

        data.SelectMenu = new KeyValuePair<string, bool> ("SelectMenu", tutorialSaveInfo["SelectMenu"]);
        data.Cafe = new KeyValuePair<string, bool>("Cafe", tutorialSaveInfo["Cafe"]);
        data.Checkout = new KeyValuePair<string, bool>("Checkout", tutorialSaveInfo["Checkout"]);
        data.Cooking = new KeyValuePair<string, bool>("Cooking", tutorialSaveInfo["Cooking"]);

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
}
