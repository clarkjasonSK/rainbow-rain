using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectHelper
{
    public static bool VerifySO<TSO>(string filePath) where TSO : GameScriptableObject
    {
        return Resources.Load<TSO>(filePath) != null ? true: false;
    }

    public static TSO GetSOGame<TSO>(string filePath) where TSO: GameScriptableObject
    {
        return Resources.Load<TSO>(filePath);
    }

    // to be removed on project build
    public static void CreateSOGame(GameScriptableObject gameSO, string path)
    {
        AssetDatabase.CreateAsset(gameSO, path);
    }

    public static T GetSOManager<T>(string filePath) where T : ScriptableObject
    {
        return Resources.Load<T>(filePath);
    }
}
