using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    [SerializeField] private Button _level_back_btn;

    private void Start()
    {
        _level_back_btn = _level_back_btn.GetComponent<Button>();
        _level_back_btn.onClick.AddListener(OnLevelsBackClicked);

    }

    #region OnClick Functions
    private void OnLevelsBackClicked()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.LEVEL_BACK_PRESSED, null);
    }
    #endregion
}
