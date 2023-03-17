using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SODataHandler
{
    private static List<ScriptableObjectsKeys> SOList;

    private static List<LevelData> _level_data_list = new List<LevelData>();
    private static List<PatternData> _pattern_data_list = new List<PatternData>();
    private static List<ProjectileData> _proj_data_list = new List<ProjectileData>();

    private static GameScriptableObject tempSO;
    private static GameData targetData;
    private static string targetName;
    public static void VerifyScriptableObjects()
    {
        SOList = JsonLoader.loadJsonData<ScriptableObjectsKeys>(FileNames.SO_LIST, false);
        //Debug.Log("SOList: " + SOList.Count + " | levelslist: " + SOList[0].LevelKeyList.Count +" | patternlist: " + SOList[1].PatternKeyList.Count + " | projlist " + SOList[2].ProjectileKeyList.Count);

        loadJsonData();

        verifySOList<LevelKey, LevelData, LevelScriptableObject>(SOList[0].LevelKeyList, _level_data_list);
        verifySOList<PatternKey, PatternData, PatternScriptableObject>(SOList[1].PatternKeyList, _pattern_data_list);
        verifySOList<ProjectileKey, ProjectileData, ProjectileScriptableObject>(SOList[2].ProjectileKeyList, _proj_data_list);

    }
    
    public static void verifySOList<TKey, TData, TSO>(List<TKey> soList, List<TData> dataList) 
        where TKey : DataKey
        where TData : GameData
        where TSO: GameScriptableObject
    {
        Debug.Log("Verifying for : " + soList[0].GetType());
        foreach (TKey key in soList)
        {
            targetName = key.SOFileName;
            if(key is LevelKey)
            {
                key.SOFileName = FileNames.LEVELS_SO_PATH + key.SOFileName;
            }
            else if (key is PatternKey)
            {
                key.SOFileName = FileNames.PATTERNS_SO_PATH + key.SOFileName;
            }
            else if (key is ProjectileKey)
            {
                key.SOFileName = FileNames.PROJECTILES_SO_PATH + key.SOFileName;
            }

            if (ScriptableObjectUtility.VerifyScriptableObject<TSO>(key.SOFileName))
            {
                //Debug.Log(" level "+ key.SOID  +"found ");
                if (key is LevelKey)
                {
                    key.SOFileName = targetName;
                    GameManager.Instance.addLevel(key as LevelKey);
                }
                continue;
            }

            Debug.Log(key.GetType()+":"+ key.SOID + " so not found. Filename: " + key.SOFileName);

            createScriptableObject<TKey, TData, TSO>(key, dataList);

            if (key is LevelKey)
            {
                key.SOFileName = targetName;
                GameManager.Instance.addLevel(key as LevelKey);
            }

        }
    }
    private static void createScriptableObject<TKey, TData, TSO>(TKey key, List<TData> dataList)
        where TKey : DataKey
        where TData : GameData
        where TSO : GameScriptableObject
    {
        tempSO = (TSO)ScriptableObject.CreateInstance(typeof(TSO));
        targetData = getTargetData<TKey, TData>(key as TKey, dataList);

        if (targetData == null)
        {
            Debug.Log("Error finding "+key.GetType()+" data with given sokeyID:"+ key.SOID);
            return;
        }

        tempSO.InstantiateData<TData>(targetData as TData);

        ScriptableObjectUtility.CreateScriptableObject(tempSO, FileNames.ASSET_RESOURCES + key.SOFileName + FileNames.ASSET_EXTENSION);

        Debug.Log("New Asset " + tempSO.GetType() + " Scriptable object created at " + key.SOFileName + FileNames.ASSET_EXTENSION + "!");

    }

    private static void loadJsonData()
    {
        _level_data_list = JsonLoader.loadJsonData<LevelData>(FileNames.LEVELS_JSON, false);
        _pattern_data_list = JsonLoader.loadJsonData<PatternData>(FileNames.PATTERNS_JSON, false);
        _proj_data_list = JsonLoader.loadJsonData<ProjectileData>(FileNames.PROJECTILES_JSON, false);
    }

    private static TData getTargetData<TKey, TData>(TKey dataID, List<TData> dataList) 
        where TKey : DataKey
        where TData : GameData
    {
        foreach (TData data in dataList)
        {
            if (dataID.SOID == data.DataID )
            {
                return data;
            }
        }

        return null;
    }

}
