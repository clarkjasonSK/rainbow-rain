using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class SODataHandler
{
    private static List<ScriptableObjectsLists> SOList;

    private static List<GameData> _data_list;

    private static List<LevelData> _level_data_list = new List<LevelData>();
    private static List<PatternData> _pattern_data_list = new List<PatternData>();
    private static List<ProjectileData> _proj_data_list = new List<ProjectileData>();

    private static GameScriptableObject tempSO;
    private static GameData targetData;
    public static void VerifyScriptableObjects()
    {
        SOList = JsonLoader.loadJsonData<ScriptableObjectsLists>(FileNames.SO_LIST, false);
        //Debug.Log("SOList: " + SOList.Count + " | levelslist: " + SOList[0].LevelSOList.Count +" | patternlist: " + SOList[1].PatternSOList.Count + " | projlist " + SOList[2].ProjectileSOList.Count);
        //verifyLevels();

        verifySOList<LevelSO>(SOList[0].LevelSOList);
        //verifySOList<PatternSO>(SOList[1].PatternSOList);
        //verifySOList<ProjectileSO>(SOList[2].ProjectileSOList);

    }
    
    public static void verifySOList<TSOK>(List<TSOK> soList) where TSOK : ScriptableObjectKey
    {
        Debug.Log("Verifying for : " + soList[0].GetType());
        foreach (TSOK key in soList)
        {
            if(key is LevelSO)
            {
                tempSO = loadScriptableObject<LevelScriptableObject>(FileNames.LEVELS_SO_PATH+ key.SOFileName);
            }
            else if (key is PatternSO)
            {
                tempSO = loadScriptableObject<PatternScriptableObject>(FileNames.PATTERNS_SO_PATH + key.SOFileName);
            }
            else if (key is ProjectileSO)
            {
                tempSO = loadScriptableObject<ProjectileScriptableObject>(FileNames.PROJECTILES_SO_PATH + key.SOFileName);
            }

            if (tempSO != null)
            {
                //Debug.Log(key.SOID + " level found " + tempSO.LevelID);
                GameManager.Instance.addLevel(key as LevelSO);
                continue;
            }

            Debug.Log(key.GetType()+":"+ key.SOID + " so not found. Filename: " + key.SOFileName);
            tempSO = null;

            if (key is LevelSO)
            {
                if (_level_data_list.Count == 0)
                {
                    loadJsonData(0);
                }

                createScriptableObject<TSOK>(key);
            }
            else if (key is PatternSO)
            {
                if (_pattern_data_list.Count == 0)
                {
                    loadJsonData(1);
                }

                createScriptableObject<TSOK>(key);
            }
            else if (key is ProjectileSO)
            {
                if (_proj_data_list.Count == 0)
                {
                    loadJsonData(2);
                }

                createScriptableObject<TSOK>(key);
            }

        }
    }
    private static void createScriptableObject<TSOK>(TSOK key) where TSOK : ScriptableObjectKey
    {
        if (key is LevelSO)
        {
            key.SOFileName = FileNames.LEVELS_SO_PATH + key.SOFileName;
            tempSO = (LevelScriptableObject)ScriptableObject.CreateInstance(typeof(LevelScriptableObject));
            targetData = getTargetData<LevelSO, LevelData>(key as LevelSO);
        }
        else if (key is PatternSO)
        {
            key.SOFileName = FileNames.PATTERNS_SO_PATH + key.SOFileName;
            tempSO = (PatternScriptableObject)ScriptableObject.CreateInstance(typeof(PatternScriptableObject));
            targetData = getTargetData<PatternSO, PatternData>(key as PatternSO);
        }
        else if (key is ProjectileSO)
        {
            key.SOFileName = FileNames.PROJECTILES_SO_PATH + key.SOFileName;
            tempSO = (ProjectileScriptableObject)ScriptableObject.CreateInstance(typeof(ProjectileScriptableObject));
            targetData = getTargetData<ProjectileSO, ProjectileData>(key as ProjectileSO);
        }


        if (targetData == null)
        {
            Debug.Log("Error finding "+key.GetType()+"data with given sokeyID");
            return;
        }

        if (key is LevelSO)
        {
            tempSO.InstantiateData<LevelData>(targetData as LevelData);
        }
        else if (key is PatternSO)
        {
            tempSO.InstantiateData<PatternData>(targetData as PatternData);
        }
        else if (key is ProjectileSO)
        {
            tempSO.InstantiateData<ProjectileData>(targetData as ProjectileData);
        }

        AssetDatabase.CreateAsset(tempSO, FileNames.ASSET_RESOURCES + key.SOFileName + FileNames.ASSET_EXTENSION);

        Debug.Log("New Asset " + tempSO.GetType() + " Scriptable object created at " + key.SOFileName + FileNames.ASSET_EXTENSION + "!");

    }

    private static TSO loadScriptableObject<TSO>(string filename) where TSO : ScriptableObject
    {
        return Resources.Load<LevelScriptableObject>(filename) as TSO;
    }

    
    // TODO: MAKE LIST FURTHER GENERIC 
    private static void loadJsonData(int dataType) // 0 = level; 1 = pattern; 2 = projectile
    {
        
        switch (dataType)
        {
            case 0: _level_data_list = JsonLoader.loadJsonData<LevelData>(FileNames.LEVELS_JSON, false); break;
            case 1: _pattern_data_list = JsonLoader.loadJsonData<PatternData>(FileNames.PATTERNS_JSON, false); break;
            case 2: _proj_data_list = JsonLoader.loadJsonData<ProjectileData>(FileNames.PROJECTILES_JSON, false); break;
        }

        //Debug.Log("levels data loaded! count: " + _level_data_list.Count);
    }

    // TODO: MAKE LIST FURTHER GENERIC 
    private static TData getTargetData<TSOK, TData>(TSOK dataID) 
        where TSOK : ScriptableObjectKey
        where TData : GameData
    {

        if(dataID is LevelSO)
        {
            foreach (LevelData lvlData in _level_data_list)
            {
                if (lvlData.DataID == dataID.SOID)
                {
                    return lvlData as TData;
                }
            }
        }

        if (dataID is PatternSO)
        {
            foreach (PatternData pttrnData in _pattern_data_list)
            {
                if (pttrnData.DataID == dataID.SOID)
                {
                    return pttrnData as TData;
                }
            }
        }

        if (dataID is ProjectileSO)
        {
            foreach (ProjectileData projData in _proj_data_list)
            {
                if (projData.DataID == dataID.SOID)
                {
                    return projData as TData;
                }
            }
        }

        return null;
    }

    /*
    public static void verifyLevels()
    {
        foreach (LevelSO levelfile in SOList[0].LevelSOList)
        {
            LevelScriptableObject tempLevelSO = Resources.Load<LevelScriptableObject>(FileNames.LEVELS_SO_PATH+ levelfile.SOFileName);

            if (tempLevelSO != null)
            {
                Debug.Log(levelfile.SOID + " level found " + tempLevelSO.LevelID);
                continue;
            }

            Debug.Log(levelfile.SOID + " levelso not found " + FileNames.LEVELS_SO_PATH + levelfile.SOFileName);

            if (_level_data_list.Count == 0)
            {
                loadJsonData(0);
            }

            createLevelScriptableObject(levelfile);

        }

    }

    private static void createLevelScriptableObject(LevelSO levelFile)
    {
        LevelScriptableObject newLSO = (LevelScriptableObject)ScriptableObject.CreateInstance(typeof(LevelScriptableObject));

        LevelData targetLevel = getTargetData<LevelSO, LevelData>(levelFile);

        if (targetLevel == null)
        {
            Debug.Log("Error finding level data with given levelfileID");
            return;
        }

        newLSO.LevelID = targetLevel.DataID;
        newLSO.LevelIsEndless = targetLevel.LevelIsEndless;
        newLSO.LevelPatterns = targetLevel.LevelPatterns;

        AssetDatabase.CreateAsset(newLSO, FileNames.ASSET_RESOURCES+FileNames.LEVELS_SO_PATH + levelFile.SOFileName + FileNames.ASSET_EXTENSION);
        Debug.Log("New Asset Level Scriptable object created at "+ FileNames.LEVELS_SO_PATH + levelFile.SOFileName + FileNames.ASSET_EXTENSION + "!");

    }*/

}
