using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : Singleton<LevelHandler>, ISingleton, IEventObserver
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private LevelScriptableObject _lvl_so;
    public LevelScriptableObject LevelSO
    {
        get { return _lvl_so; }
    }

    [SerializeField] private List<PatternScriptableObject> _pattern_so_list;

    private float _level_elapsed_time;
    private float _level_total_time;


    public void Initialize()
    {
        _lvl_so = SODataHandler.getCurrentLevelSO();
        _pattern_so_list = SODataHandler.getSOList<PatternScriptableObject>(_lvl_so.LevelPatterns, 1);


        AddEventObservers();
    }

    public void AddEventObservers()
    {

    }
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;


    }
}
