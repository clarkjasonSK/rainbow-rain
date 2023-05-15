using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUIHandler : MonoBehaviour, IEventObserver
{
    [SerializeField] private GameObject _level_button_template;
    [SerializeField] private Transform _level_content_transform;

    public void Awake()
    {
        CreateLevelButtons(GameManager.Instance.LevelsList);
        AddEventObservers();
    }

    public  void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVEL_PRESSED, OnLevelPressed);
    }

    private void CreateLevelButtons(List<LevelKey> lvlList)
    {
        foreach (LevelKey lvlkey in lvlList)
        {
            GameObject newObj = Instantiate(_level_button_template, _level_content_transform);
            newObj.SetActive(true);

            LevelButton newLevel = newObj.GetComponent<LevelButton>();

            newLevel.LevelID = lvlkey.SOID;
            newLevel.LevelName = lvlkey.SOFileName;

            newObj.GetComponentInChildren<TextMeshProUGUI>().text = newLevel.LevelName;

        }
    }
    private void SetCurrentLevelSO(int levelID)
    {
        SODataHandler.SetCurrentLevelSO(levelID);
    }

    #region Event Broadcaster Notifications
    private void OnLevelPressed(EventParameters param)
    {
        SetCurrentLevelSO( param.GetParameter<int>(EventParamKeys.LEVEL_ID, 0) );
        UIManager.Instance.StartGame();
    }

    #endregion


}
