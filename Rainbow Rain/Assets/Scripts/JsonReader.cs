using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class JsonReader
{
    public static string readJSONFile(string filename, bool requiresFullPath)
    {

        if (requiresFullPath)
        {
            filename = getFullPath(filename);
        }

        if (!File.Exists(filename))
        {
            Debug.Log("Error, file not found: " + filename);
            return "";
        }

        using (StreamReader stream = new StreamReader(filename))
        {
            Debug.Log("Reading JSON file...");
            return stream.ReadToEnd();
        }

    }
    private static string getFullPath(string filename)
    {
        Debug.Log("Full Path: " + Application.persistentDataPath + "/" + filename);
        return Application.persistentDataPath + "/" + filename;
    }
}

public static class JsonHelper
{
    public static List<T> FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items.ToList();
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}
