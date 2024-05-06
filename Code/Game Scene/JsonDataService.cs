using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;    

// uses the IDataService interface
public class JsonDataService : IDataService
{
    // method for saving user data to Json file
    public bool SaveData<T>(string RelativePath, T Data)
    {
        string path = Application.persistentDataPath + RelativePath;

     
        try
        {   
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data));
            return true;
        }
        catch(Exception e)
        {
            Debug.Log("Unable save data");
            return false;
        }

    }

    // method for creating a new Json file
    public bool CreateNewFile<T>(string RelativePath, T Data)
    {
    string path = Application.persistentDataPath + RelativePath;

    try
    {
        if (!File.Exists(path))
        {
        // Write initial data as an empty dictionary
        string initialData = "{}";
        using (FileStream stream = File.Create(path))
        {
            stream.Close();
            File.WriteAllText(path, initialData);
        }
        }
        return true;
    }
    catch (Exception e)
    {
        Debug.Log("Unable save data");
        return false;
    }
    }

    // method for loading user data from Json file
    public T LoadData<T>(string RelativePath)
    {
        string path = Application.persistentDataPath + RelativePath;

        if (!File.Exists(path))
        {
            Debug.LogError("FIle doesn't exist");
            throw new FileNotFoundException("Path does not exist");
        }
        try 
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch(Exception e)
        {
            Debug.LogError("FIle doesn't exist");
            throw e;
        }
    }
}
