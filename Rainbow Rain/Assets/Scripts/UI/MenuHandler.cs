using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuHandler: MonoBehaviour
{
    [SerializeField] private GameObject _main_canvas;

    #region Transform Panels
    [SerializeField] private GameObject _top_panel;
    [SerializeField] private GameObject _mid_panel;
    [SerializeField] private GameObject _btm_panel;
    #endregion

    #region Menu Panels
    [SerializeField] private GameObject _main_menu_panel;
    [SerializeField] private GameObject _level_panel;
    [SerializeField] private GameObject _credits_panel;
    [SerializeField] private GameObject _settings_panel;
    #endregion

    #region Buttons
    [SerializeField] private Button _play_btn;
    [SerializeField] private Button _credits_btn;
    [SerializeField] private Button _settings_btn;

    [SerializeField] private Button _level_back_btn;
    #endregion

    #region Event Parameters
    private EventParameters menuParams;
    #endregion

    public void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        UIManager.Instance.MenuHandler = this;
        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick .AddListener(OnPlayClicked);

        _credits_btn = _credits_btn.GetComponent<Button>();
        _credits_btn.onClick.AddListener(OnCreditsClicked);

        _settings_btn = _settings_btn.GetComponent<Button>();
        _settings_btn.onClick.AddListener(OnSettingsClicked);

        _level_back_btn = _level_back_btn.GetComponent<Button>();
        _level_back_btn.onClick.AddListener(OnLevelsBackClicked);

        /*
        Debug.Log("main_menu tnsfm: " + _main_menu_panel.transform.position);
        Debug.Log("_mid_panel tnsfm: " + _mid_panel.transform.position);
        Debug.Log("_level_panel tnsfm: " + _level_panel.transform.position);
        Debug.Log("_top_panel tnsfm: " + _top_panel.transform.position);*/

        _main_menu_panel.transform.position = _mid_panel.transform.position;
        _level_panel.transform.position = _top_panel.transform.position;
        //_level_panel.SetActive(false);

        menuParams = new EventParameters();

        menuParams.AddParameter<GameObject>(EventParamKeys.MAIN_PANEL_PARAM, _main_menu_panel);
        menuParams.AddParameter<GameObject>(EventParamKeys.LEVEL_PANEL_PARAM, _level_panel);

        menuParams.AddParameter<GameObject>(EventParamKeys.TOP_PANEL_PARAM, _top_panel);
        menuParams.AddParameter<GameObject>(EventParamKeys.MID_PANEL_PARAM, _mid_panel);
        menuParams.AddParameter<GameObject>(EventParamKeys.BTM_PANEL_PARAM, _btm_panel);

        DontDestroyOnLoad(this.transform);
    }

    public void ToggleVisibility()
    {
        _main_canvas.SetActive( _main_canvas.activeInHierarchy == true ? false : true);
    }
    #region OnClick Functions
    private void OnPlayClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.PLAY_PRESSED, menuParams);
    }

    private void OnLevelsBackClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.LEVEL_BACK_PRESSED, menuParams);
    }

    private void OnCreditsClicked()
    {
        _credits_panel.SetActive(true);
    }

    private void OnSettingsClicked()
    {
        _settings_panel.SetActive(true);
    }
    #endregion


}
