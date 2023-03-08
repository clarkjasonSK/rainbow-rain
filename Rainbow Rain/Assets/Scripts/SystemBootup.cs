using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemBootup
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadSystem()
    {
        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();

        UIManager.Instance.Initialize();
        ColorDictionary.InitializeColors();

        LevelDataSOHandler.loadLevelsList();

        if (EventBroadcaster.Instance.IsDoneInitializing &&
            GameManager.Instance.IsDoneInitializing &&
            UIManager.Instance.IsDoneInitializing)
        {
            Debug.Log(SceneNames.MAIN_MENU + " initialized!");
            EventBroadcaster.Instance.PostEvent(EventKeys.START_MENU, null);
        }
    }

}
