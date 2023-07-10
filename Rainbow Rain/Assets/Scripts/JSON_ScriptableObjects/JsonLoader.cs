using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
public static class JsonLoader
{
    public static List<T> loadJsonData<T>(string filename, bool requiresFullPath)
    {

        string jsonInfo = JsonReader.readJSONFile(filename, requiresFullPath);

        if (string.IsNullOrEmpty(jsonInfo) || jsonInfo == "")
        {
            Debug.Log("Error loading data");
            return null;
        }

        List<T> tempList = JsonHelper.FromJson<T>(jsonInfo);

        //Debug.Log("List count: " + tempList.Count);
        return tempList;
    }

    /*
    public static List<ProjJSONData> loadProjJSONData<T>(string filename, bool requiresFullPath)
    {
        string jsonInfo = JsonReader.readJSONFile(filename, requiresFullPath);

        if(string.IsNullOrEmpty(jsonInfo) || jsonInfo == "")
        {
            Debug.Log("Error loading projectiles");
            return null;
        }

        List<ProjJSONData> tempList = JsonHelper.FromProjectilesJson<ProjJSONData>(jsonInfo);
        
        Debug.Log("Projectiles count: " + tempList.Count);
        return tempList;
    }


    public static List<PatternData> loadPatternData<T>(string filename, bool requiresFullPath)
    {

        string jsonInfo = JsonReader.readJSONFile(filename, requiresFullPath);

        if (string.IsNullOrEmpty(jsonInfo) || jsonInfo == "")
        {
            Debug.Log("Error loading projectiles");
            return null;
        }

        List<PatternData> tempList = JsonHelper.FromPatternsJson<PatternData>(jsonInfo);

        Debug.Log("Pattern count: " + tempList.Count);
        return tempList;
    }*/

    
}
