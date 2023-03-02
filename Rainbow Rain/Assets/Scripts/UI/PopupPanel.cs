using UnityEngine;
using UnityEngine.UI;

public class PopupPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    [SerializeField] private Button _exit_button;

    void Start()
    {
        _exit_button = _exit_button.GetComponent<Button>();
        _exit_button.onClick.AddListener(OnExitClicked);
    }
    private void OnExitClicked()
    {
        _panel.SetActive(false);
    }
}
