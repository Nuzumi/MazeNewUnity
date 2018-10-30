using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadDataController  {

    public static StringEvent debugEvent;
    public static string fileName = "playerData.pd";

    private static SaveLoadData loadedData;
    public static SaveLoadData LoadedData
    {
        get
        {
            if (loadedData == null)
                loadedData = LoadData();

            return loadedData;
        }

        set
        {
            loadedData = value;
            SaveData(value);
        }
    }

    private static bool SaveData(SaveLoadData saveLoadData)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        try
        {
            FileStream file = File.Create(Path.Combine(Application.persistentDataPath, fileName));
            binaryFormatter.Serialize(file, saveLoadData ?? new SaveLoadData());
            file.Close();
        }
        catch (Exception ex)
        {
            debugEvent.Invoke(ex.Message);
            Debug.Log(ex.Message);
        }
        return true;
    }

    private static SaveLoadData LoadData()
    {

        if(File.Exists(Path.Combine(Application.persistentDataPath,fileName)))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            SaveLoadData data = null;
            try
            {
                FileStream file = File.Open(Path.Combine(Application.persistentDataPath, fileName), FileMode.Open);
                data = (SaveLoadData)binaryFormatter.Deserialize(file);
                file.Close();
            }
            catch(Exception ex)
            {
                Debug.Log(ex.Message);
                debugEvent.Invoke(ex.Message);
            }
            
            return data;
        }
        else
        {
            Debug.Log("noFileToLoad");
            SaveData(loadedData);
            return LoadData();
        }


    }

    public static void SaveData()
    {
        SaveData(LoadedData);
    }

    public static void ClearData()
    {
        LoadedData = new SaveLoadData();
    }
}
