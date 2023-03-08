using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "ScriptableObjects/Pattern")]
public class PatternScriptableObject: ScriptableObject
{
    #region Pattern Values
    [SerializeField] private int _pattern_id = 0;
    public int PatternID
    {
        get { return _pattern_id; }
        set { _pattern_id = value; }
    }

    [SerializeField] private bool _pattern_repeatable;
    public bool PatternRepeatable
    {
        get { return _pattern_repeatable; }
    }

    [SerializeField] private float _pattern_duration;
    public float PatternDuration
    {
        get { return _pattern_duration; }
    }

    [SerializeField] private List<int> _pattern_proj_list;
    public List<int> PatternProjectileList
    {
        get { return _pattern_proj_list; }
    }
    public int PatternProjectileSize
    {
        get { return _pattern_proj_list.Count;}
    }
    #endregion

    void OnEnable()
    {
        _pattern_proj_list = new List<int>();
    }

    public void addProjectile(int proj)
    {
        _pattern_proj_list.Add(proj);
    }

}
