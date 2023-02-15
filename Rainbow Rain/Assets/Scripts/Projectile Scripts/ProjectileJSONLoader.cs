using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity;
public static class ProjectileJSONLoader
{
    public static List<ProjectileInfo> loadJSONInfo<T>(string filename, bool requiresFullPath)
    {
        string jsonInfo = JsonReader.readJSONFile(filename, requiresFullPath);

        if(string.IsNullOrEmpty(jsonInfo) || jsonInfo == "")
        {
            Debug.Log("Error loading projectiles");
            return null;
        }

        List<ProjectileInfo> tempList = JsonHelper.FromJson<ProjectileInfo>(jsonInfo);
        
        Debug.Log("Projectiles count: " + tempList.Count);
        return tempList;
    }
}
