using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuManager: Singleton<MenuManager>, ISingleton
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    [SerializeField] private GameObject _top_panel;
    [SerializeField] private GameObject _mid_panel;
    [SerializeField] private GameObject _btm_panel;

    [SerializeField] private float _panel_move_speed;

    [SerializeField] private GameObject _main_menu_panel;

    [SerializeField] private Button _play_btn;
    [SerializeField] private Button _credits_btn;
    [SerializeField] private Button _settings_btn;

    [SerializeField] private GameObject _level_panel;
    [SerializeField] private Button _level_back_btn;

    [SerializeField] private GameObject _credits_panel;
    [SerializeField] private GameObject _settings_panel;

    private IEnumerator _coroutine_1;
    private IEnumerator _coroutine_2;

    public void Initialize()
    {
        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick .AddListener(OnPlayClicked);

        _credits_btn = _credits_btn.GetComponent<Button>();
        _credits_btn.onClick.AddListener(OnCreditsClicked);

        _settings_btn = _settings_btn.GetComponent<Button>();
        _settings_btn.onClick.AddListener(OnSettingsClicked);

        _level_back_btn = _level_back_btn.GetComponent<Button>();
        _level_back_btn.onClick.AddListener(OnLevelsBackClicked);

        _main_menu_panel.transform.position = _mid_panel.transform.position;
        _level_panel.transform.position = _btm_panel.transform.position;

        DontDestroyOnLoad(this.transform);


    }

    private void OnPlayClicked()
    {
        _coroutine_1 = scrollUpPanel(_main_menu_panel, _top_panel);
        StartCoroutine(_coroutine_1);

        _coroutine_2 = scrollUpPanel(_level_panel, _mid_panel);
        StartCoroutine(_coroutine_2);

        /*scrollDownCoroutine = scrollDownPanel(_main_menu_panel);
        StartCoroutine(scrollUpCoroutine);*/
        //UIManager.Instance.goToGame();
    }

    private void OnLevelsBackClicked()
    {
        _coroutine_1 = scrollDownPanel(_main_menu_panel, _mid_panel);
        StartCoroutine(_coroutine_1);

        _coroutine_2 = scrollDownPanel(_level_panel, _btm_panel);
        StartCoroutine(_coroutine_2);
    }

    private void OnCreditsClicked()
    {
        _credits_panel.SetActive(true);
    }
    private void OnSettingsClicked()
    {
        _settings_panel.SetActive(true);
    }

    IEnumerator scrollUpPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        //Debug.Log("scrollUpPanel starting! " + panel.transform.position + " to "+_top_transform_panel.transform.position);

        while (movingPanel.transform.position.y < destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, _panel_move_speed * Time.deltaTime));
            yield return null;
        }
        StopCoroutine("scrollUpPanel");
    }

    IEnumerator scrollDownPanel(GameObject movingPanel, GameObject destinationPanel)
    {
        while (movingPanel.transform.position.y > destinationPanel.transform.position.y)
        {
            movingPanel.transform.position = new Vector2(0, Mathf.MoveTowards(movingPanel.transform.position.y, destinationPanel.transform.position.y, _panel_move_speed * Time.deltaTime));
            yield return null;
        }
        StopCoroutine("scrollDownPanel");
    }
}
