using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainMenuHandler: Handler
{
    [SerializeField] private MainMenuRefs _main_menu_refs;

    [SerializeField] private VisualSettings _visual_settings;

    #region Coroutines Variables
    private IEnumerator _coroutine_a;
    private IEnumerator _coroutine_b;
    #endregion

    public override void Initialize()
    {
        if (_main_menu_refs is null)
            _main_menu_refs = GetComponent<MainMenuRefs>();

        _visual_settings = GameManager.Instance.VisualSettings;

        _main_menu_refs.TitlePanel.transform.position = _main_menu_refs.MidPanel.transform.position;
        _main_menu_refs.LevelPanel.transform.position = _main_menu_refs.TopPanel.transform.position;

        AddEventObservers();
        //DontDestroyOnLoad(this.transform);
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAY_PRESSED, OnPlayPressed);
        EventBroadcaster.Instance.AddObserver(EventKeys.LEVEL_BACK_PRESSED, OnBackLevelPressed);
    }

    
    #region Event Broadcaster Notifications
    public void OnPlayPressed(EventParameters param = null)
    {
        _coroutine_a = scrollDownPanel(
            _main_menu_refs.TitlePanel.gameObject, _main_menu_refs.BottomPanel);
        _coroutine_b = scrollDownPanel(
            _main_menu_refs.LevelPanel.gameObject, _main_menu_refs.MidPanel);

        StartCoroutine(_coroutine_a);
        StartCoroutine(_coroutine_b);
    }
    public void OnBackLevelPressed(EventParameters param)
    {
        _coroutine_a = scrollUpPanel(_main_menu_refs.TitlePanel.gameObject, _main_menu_refs.MidPanel);
        _coroutine_b = scrollUpPanel(
            _main_menu_refs.LevelPanel.gameObject, _main_menu_refs.TopPanel);

        StartCoroutine(_coroutine_a);
        StartCoroutine(_coroutine_b);
    }
    #endregion

    #region Coroutines
    public IEnumerator scrollUpPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        //Debug.Log("scrollUpPanel starting! " + panel.transform.position + " to "+_top_transform_panel.transform.position);

        while (movingPanel.transform.position.y < destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, _visual_settings.PanelMoveSpeed * Time.deltaTime));
            yield return null;
        }

        yield break;
    }

    public IEnumerator scrollDownPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        while (movingPanel.transform.position.y > destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, _visual_settings.PanelMoveSpeed * Time.deltaTime));
            yield return null;
        }
        yield break;
    }
    #endregion

    
    public void ToggleUI(bool toggle)
    {
        _main_menu_refs.MainCanvas.SetActive(toggle);
    }


}
