using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootup : MonoBehaviour
{
    void Awake()
    {
        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();

        UIManager.Instance.Initialize();
        ColorDictionary.InitializeColors();

        if(EventBroadcaster.Instance.IsDoneInitializing &&
            GameManager.Instance.IsDoneInitializing &&
            UIManager.Instance.IsDoneInitializing)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.START_MENU, null);
        }
    }

}
