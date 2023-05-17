using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton: MonoBehaviour
{
    [SerializeField] private Button _level_button;

    #region Level Values
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
    #endregion

    #region Event Parameters
    private EventParameters levelParams;
    #endregion

    public void Start()
    {

        _level_button = _level_button.GetComponent<Button>();
        _level_button.onClick.AddListener(OnLevelClicked);

        levelParams = new EventParameters();
        levelParams.AddParameter(EventParamKeys.LEVEL_ID, _level_id);
    }
    private void OnLevelClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.LEVEL_PRESSED, levelParams);
    }
}
