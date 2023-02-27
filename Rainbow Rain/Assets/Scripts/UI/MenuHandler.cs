using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuHandler : MonoBehaviour
{
    [SerializeField] private Button _play_btn;
    [SerializeField] private Button _custom_btn;
    [SerializeField] private Button _credits_btn;


    private void Start()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        _play_btn = _play_btn.GetComponent<Button>();
        _play_btn.onClick.AddListener(OnPlayClicked);

        _custom_btn = _custom_btn.GetComponent<Button>();
        _custom_btn.onClick.AddListener(OnCustomClicked);

        _credits_btn = _credits_btn.GetComponent<Button>();
        _credits_btn.onClick.AddListener(OnCreditsClicked);

    }

    private void OnPlayClicked()
    {
        UIManager.Instance.goToGame();
    }
    private void OnCustomClicked()
    {

    }
    private void OnCreditsClicked()
    {

    }
}
