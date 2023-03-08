using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class LevelDataSOHandler
{
    private static List<LevelData> _level_data_list = new List<LevelData>();
    public static void loadLevelsList()
    {
        List<LevelSO> levelSOlist = JsonLoader.loadJsonData<LevelSO>(FileNames.LEVELS_SO_LIST, false);

        //Debug.Log("levelSO list count: " + levelSOlist.Count);

        foreach(LevelSO levelfile in levelSOlist)
        {
            LevelScriptableObject tempLevelSO = Resources.Load<LevelScriptableObject>(FileNames.LEVELS_SO_PATH+ levelfile.LevelFileName);

            if (tempLevelSO != null)
            {
                Debug.Log(levelfile.LevelID + "level found " + tempLevelSO.LevelID);
                continue;
            }

            Debug.Log(levelfile.LevelID + " levelso not found " + FileNames.LEVELS_SO_PATH + levelfile.LevelFileName);

            if (_level_data_list.Count == 0)
            {
                loadLevelData();
            }

            createScriptableObject(levelfile, tempLevelSO);

        }

    }
    private static void loadLevelData()
    {
        _level_data_list = JsonLoader.loadJsonData<LevelData>(FileNames.LEVELS_JSON, false);
        //Debug.Log("levels data loaded! count: " + _level_data_list.Count);
    }

    private static void createScriptableObject(LevelSO levelFile, LevelScriptableObject tempLevelSO)
    {
        LevelScriptableObject newLSO = (LevelScriptableObject)ScriptableObject.CreateInstance(typeof(LevelScriptableObject));

        LevelData targetLevel = getLevelData(levelFile.LevelID);

        if (targetLevel == null)
        {
            Debug.Log("Error finding level data with given levelfileID");
            return;
        }

        newLSO.LevelID = targetLevel.LevelID;
        newLSO.LevelIsEndless = targetLevel.LevelIsEndless;
        newLSO.LevelPatterns = targetLevel.LevelPatterns;

        AssetDatabase.CreateAsset(newLSO, FileNames.ASSET_RESOURCES+FileNames.LEVELS_SO_PATH + levelFile.LevelFileName+FileNames.ASSET_EXTENSION);
        Debug.Log("New Asset Level Scriptable object created at "+ FileNames.LEVELS_SO_PATH + levelFile.LevelFileName + FileNames.ASSET_EXTENSION + "!");

    }

    private static LevelData getLevelData(int levelID)
    {
        foreach(LevelData lvlData in _level_data_list)
        {
            if(lvlData.LevelID == levelID)
            {
                return lvlData;
            }
        }
        return null;
    }
}
