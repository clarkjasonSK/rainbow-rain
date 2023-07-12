using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SODataHandler
{
    #region DataLists
    private static List<ScriptableObjectsKeys> SOList;

    private static List<LevlJSONData> _level_data_list = new List<LevlJSONData>();
    private static List<PattJSONData> _pattern_data_list = new List<PattJSONData>();
    private static List<ProjJSONData> _proj_data_list = new List<ProjJSONData>();
    #endregion

    #region Utility Variables
    private static GameScriptableObject tempSO;
    private static JSONData targetData;
    private static string targetName;

    private static List<GameScriptableObject> getDataList;
    //private static LevelScriptableObject currentLevel;
    //private static LevelScriptableObject targetLevel;
    #endregion



    public static void VerifyScriptableObjects()
    {
        SOList = JsonLoader.loadJsonData<ScriptableObjectsKeys>(FileNames.SO_LIST, false);
        //Debug.Log("SOList: " + SOList.Count + " | levelslist: " + SOList[0].LevelKeyList.Count +" | patternlist: " + SOList[1].PatternKeyList.Count + " | projlist " + SOList[2].ProjectileKeyList.Count);

        loadJsonData();

        verifySOList<LevelKey, LevlJSONData, LevelScriptableObject>(SOList[0].LevelKeyList, _level_data_list);
        verifySOList<PatternKey, PattJSONData, PatternScriptableObject>(SOList[1].PatternKeyList, _pattern_data_list);
        verifySOList<ProjectileKey, ProjJSONData, ProjectileScriptableObject>(SOList[2].ProjectileKeyList, _proj_data_list);

    }
    
    private static void verifySOList<TKey, TData, TSO>(List<TKey> soList, List<TData> dataList) 
        where TKey : DataKey
        where TData : JSONData
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

            if (ScriptableObjectHelper.VerifySO<TSO>(key.SOFileName))
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
        where TData : JSONData
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

        ScriptableObjectHelper.CreateSOGame(tempSO, FileNames.ASSET_RESOURCES + key.SOFileName + FileNames.ASSET_EXTENSION);

        Debug.Log("New Asset " + tempSO.GetType() + " Scriptable object created at " + key.SOFileName + FileNames.ASSET_EXTENSION + "!");

    }

    private static void loadJsonData()
    {
        _level_data_list = JsonLoader.loadJsonData<LevlJSONData>(FileNames.LEVELS_JSON, false);
        _pattern_data_list = JsonLoader.loadJsonData<PattJSONData>(FileNames.PATTERNS_JSON, false);
        _proj_data_list = JsonLoader.loadJsonData<ProjJSONData>(FileNames.PROJECTILES_JSON, false);
    }

    private static TData getTargetData<TKey, TData>(TKey dataID, List<TData> dataList) 
        where TKey : DataKey
        where TData : JSONData
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

    public static LevelScriptableObject getCurrentLevelSO()
    {
        return ScriptableObjectHelper.GetSOGame<LevelScriptableObject>(FileNames.CURRENT_LEVEL_SO);
    }
    public static void SetCurrentLevelSO(int levelID)
    {
        ScriptableObjectHelper.GetSOGame<LevelScriptableObject>(FileNames.CURRENT_LEVEL_SO).InstantiateData<LevlJSONData>(getLevelData(levelID));

    }

    private static LevlJSONData getLevelData(int LevelID)
    {
        foreach (LevlJSONData lvl in _level_data_list)
        {
            if (LevelID == lvl.DataID)
            {
                return lvl;
            }
        }

        return null;
    }

    public static List<TSO> getSOList<TSO>(List<int> keyList, int keyType)
        where TSO : GameScriptableObject
    {
        Debug.Log("sifling thru " + keyList.Count + " for a" + keyType);

        List<TSO> tempList = new List<TSO>();
        foreach (int key in keyList)
        {
            tempList.Add(ScriptableObjectHelper.GetSOGame<TSO>(getSOName(key, keyType))); 
        }
        return tempList;
    }

    private static string getSOName(int key, int keyType)
    {
        switch (keyType) // 1 = Pattern | 2 = Projectile
        {
            case 1:
                foreach (PatternKey pttrn in SOList[1].PatternKeyList)
                {
                    Debug.Log("searching thru patternkeylist...");
                    if (key == pttrn.SOID)
                    {
                        Debug.Log("returning so: " + pttrn.SOFileName);
                        return pttrn.SOFileName;
                    }
                }
                break;
            case 2:
                foreach (ProjectileKey proj in SOList[2].ProjectileKeyList)
                {
                    Debug.Log("searching thru projkeylist...");
                    if (key == proj.SOID)
                    {
                        Debug.Log("returning so: " + proj.SOFileName);
                        return proj.SOFileName;
                    }
                }
                break;
        }
        return "Name Not Found";
    }

    /*
    private static TData getJSONData<TData>(int keyID, int keyType)
        where TData : JSONData
    {
        switch (keyType) // 0 = Level | 1 = Pattern | 2 = Projectile
        {
            case 0:
                foreach (LevelData lvl in _level_data_list)
                {
                    if (keyID == lvl.DataID)
                    {
                        return lvl as TData;
                    }
                }
                break;
            case 1:
                foreach (PatternData pttrn in _pattern_data_list)
                {
                    if (keyID == pttrn.DataID)
                    {
                        return pttrn as TData;
                    }
                }
                break;
            case 2:
                foreach (ProjJSONData proj in _proj_data_list)
                {
                    if (keyID == proj.DataID)
                    {
                        return proj as TData;
                    }
                }
                break;
        }
        
        return null;
    }*/

}
