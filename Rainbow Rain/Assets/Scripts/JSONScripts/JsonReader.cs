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
            Debug.Log("Reading file ["+ filename+"]...");
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
        DataWrapper<T> wrapper = JsonUtility.FromJson<DataWrapper<T>>(json);
        return wrapper.Data.ToList();
    }

    [Serializable]
    private class DataWrapper<T>
    {
        public T[] Data;
    }

    /*
    public static List<T> FromProjectilesJson<T>(string json)
    {
        ProjectileWrapper<T> wrapper = JsonUtility.FromJson<ProjectileWrapper<T>>(json);
        return wrapper.Projectiles.ToList();
    }

    [Serializable]
    private class ProjectileWrapper<T>
    {
        public T[] Projectiles;
    }


    public static List<T> FromPatternsJson<T>(string json)
    {
        PatternWrapper<T> wrapper = JsonUtility.FromJson<PatternWrapper<T>>(json);
        return wrapper.Patterns.ToList();
    }

    [Serializable]
    private class PatternWrapper<T>
    {
        public T[] Patterns;
    }

    public static List<T> FromLevelsJson<T>(string json)
    {
        LevelWrapper<T> wrapper = JsonUtility.FromJson<LevelWrapper<T>>(json);
        return wrapper.Levels.ToList();
    }

    [Serializable]
    private class LevelWrapper<T>
    {
        public T[] Levels;
    }
    */
}
