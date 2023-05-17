using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private GameObject _title_panel;
    [SerializeField] private GameObject _credits_panel;
    [SerializeField] private GameObject _settings_panel;

    [SerializeField] private Button _play_btn;
    [SerializeField] private Button _credits_btn;
    [SerializeField] private Button _settings_btn;

    private void Start()
    {

        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick.AddListener(OnPlayClicked);

        _credits_btn = _credits_btn.GetComponent<Button>();
        _credits_btn.onClick.AddListener(OnCreditsClicked);

        _settings_btn = _settings_btn.GetComponent<Button>();
        _settings_btn.onClick.AddListener(OnSettingsClicked);

    }

    #region OnClick Functions
    private void OnPlayClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.PLAY_PRESSED, null);
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
