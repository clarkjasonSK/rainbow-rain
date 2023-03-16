using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectUtility
{
    public static bool VerifyScriptableObject<TSO>(string filePath) where TSO : GameScriptableObject
    {
        return Resources.Load<TSO>(filePath) != null ? true: false;
    }

    public static TSO GetScriptableObject<TSO>(string filePath) where TSO: GameScriptableObject
    {
        return Resources.Load<TSO>(filePath);
    }

    public static void CreateScriptableObject(GameScriptableObject gameSO, string path)
    {
        AssetDatabase.CreateAsset(gameSO, path);
    }
    
}
