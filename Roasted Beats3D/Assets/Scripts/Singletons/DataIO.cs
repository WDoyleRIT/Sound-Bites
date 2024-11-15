using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataIO : Singleton<DataIO>
{
    public void SavaData<T>(T data, string fullPath)
    {
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

    public bool CheckFilePath(string fullPath)
    {
        return File.Exists(fullPath);
    }

    public T LoadData<T>(string fullPath)
    {
        string data = "";

        using (FileStream stream = new FileStream(fullPath, FileMode.Open))
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                data = reader.ReadToEnd();
            }
        }

        T loadedData = JsonUtility.FromJson<T>(data);

        return loadedData;
    }
}
