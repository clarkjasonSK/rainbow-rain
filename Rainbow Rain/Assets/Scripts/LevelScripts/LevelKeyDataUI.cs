using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelKeyDataUI : MonoBehaviour
{
    [SerializeField] private int _level_id;
    public int LevelID 
    { 
        get { return _level_id; }
        set { _level_id = value; }
    }

    [SerializeField] private string _level_name;
    public string LevelName 
    { 
        get { return _level_name; }
        set { _level_name = value; }
    }
}
