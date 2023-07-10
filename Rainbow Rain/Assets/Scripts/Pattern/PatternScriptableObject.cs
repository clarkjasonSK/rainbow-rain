using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pattern", menuName = "ScriptableObjects/Pattern")]
public class PatternScriptableObject: GameScriptableObject
{
    #region Pattern Values
    [SerializeField] private int _pattern_id = 0;
    public int PatternID
    {
        get { return _pattern_id; }
    }

    [SerializeField] private string _pattern_name;
    public string PatternName
    {
        get { return _pattern_name; }
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
    public List<int> PatternProjectiles
    {
        get { return _pattern_proj_list; }
    }
    public int PatternProjectileSize
    {
        get { return _pattern_proj_list.Count; }
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


    public override void InstantiateData<TData>(TData JSONData)
    {
        assignValues(JSONData as PatternData);
    }
    public void assignValues(PatternData pttrnData)
    {
        _pattern_id = pttrnData.DataID;
        _pattern_name = pttrnData.DataName;

        _pattern_repeatable = pttrnData.PatternRepteatable;
        _pattern_duration = pttrnData.PatternDuration;

        _pattern_proj_list = pttrnData.PatternProjectiles;

    }
}
