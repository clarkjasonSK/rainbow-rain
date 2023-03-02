using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Button _play_btn;

    [SerializeField] private GameObject _credits_panel;
    [SerializeField] private Button _credits_btn;

    [SerializeField] private GameObject _settings_panel;
    [SerializeField] private Button _settings_btn;


    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick .AddListener(OnPlayClicked);

        _credits_btn = _credits_btn.GetComponent<Button>();
        _credits_btn.onClick.AddListener(OnCreditsClicked);

        _settings_btn = _settings_btn.GetComponent<Button>();
        _settings_btn.onClick.AddListener(OnSettingsClicked);


    }

    private void OnPlayClicked()
    {
        UIManager.Instance.goToGame();
    }

    private void OnCreditsClicked()
    {
        _credits_panel.SetActive(true);
    }
    private void OnSettingsClicked()
    {
        _settings_panel.SetActive(true);
    }
}
