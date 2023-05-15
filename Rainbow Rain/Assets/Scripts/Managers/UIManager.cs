using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, ISingleton, IEventObserver
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private float PanelMoveSpeed = 45;

    #region Coroutines Variables
    private IEnumerator _coroutine_1;
    private IEnumerator _coroutine_2;
    #endregion

    private MenuHandler _menu_handler;
    public MenuHandler MenuHandler
    {
        set { _menu_handler = value; }
    }

    public void Initialize()
    {
        AddEventObservers();

        isDone = true;
    }

    public void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVEL_BACK_PRESSED, OnBackLevelPressed);
    }

    public void StartGame()
    {
        _menu_handler.ToggleVisibility();
        EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
    }

    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param)
    {
        _coroutine_1 = scrollDownPanel(
            param.GetParameter<GameObject>(EventParamKeys.MAIN_PANEL_PARAM, null), param.GetParameter<GameObject>(EventParamKeys.BTM_PANEL_PARAM, null));
        _coroutine_2 = scrollDownPanel(
            param.GetParameter<GameObject>(EventParamKeys.LEVEL_PANEL_PARAM, null), param.GetParameter<GameObject>(EventParamKeys.MID_PANEL_PARAM, null));

        StartCoroutine(_coroutine_1);
        StartCoroutine(_coroutine_2);
    }
    public void OnBackLevelPressed(EventParameters param)
    {
        _coroutine_1 = scrollUpPanel(
            param.GetParameter<GameObject>(EventParamKeys.MAIN_PANEL_PARAM, null), param.GetParameter<GameObject>(EventParamKeys.MID_PANEL_PARAM, null));
        _coroutine_2 = scrollUpPanel(
            param.GetParameter<GameObject>(EventParamKeys.LEVEL_PANEL_PARAM, null), param.GetParameter<GameObject>(EventParamKeys.TOP_PANEL_PARAM, null));

        StartCoroutine(_coroutine_1);
        StartCoroutine(_coroutine_2);
    }

    #endregion

    #region Coroutines
    public IEnumerator scrollUpPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        //Debug.Log("scrollUpPanel starting! " + panel.transform.position + " to "+_top_transform_panel.transform.position);

        while (movingPanel.transform.position.y < destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, PanelMoveSpeed * Time.deltaTime));
            yield return null;
        }

        StopCoroutine("scrollUpPanel");
    }

    public IEnumerator scrollDownPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        while (movingPanel.transform.position.y > destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, PanelMoveSpeed * Time.deltaTime));
            yield return null;
        }
        StopCoroutine("scrollDownPanel");
    }
    #endregion
    
    private GameObject togglePanel(GameObject panel)
    {
        panel.SetActive( panel.activeInHierarchy == false ? true : false);
        return panel;
    }
}
