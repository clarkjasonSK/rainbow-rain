using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level")]
public class LevelScriptableObject : ScriptableObject
{
    #region Level Values
    [SerializeField] private int _level_id = 0;
    public int LevelID
    {
        get { return _level_id; }
        set { _level_id = value; }
    }


    [SerializeField] private bool _is_endless=false;
    public bool LevelIsEndless
    {
        get { return _is_endless; }
        set { _is_endless = value; }
    }


    [SerializeField] private List<int> _level_patterns;
    public List<int> LevelPatterns
    {
        get { return _level_patterns; }
        set { _level_patterns = value; }
    }
    public int LevelPatternSize
    {
        get { return _level_patterns.Count; }
    }
    #endregion
}
