using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SystemBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void LoadSystem()
    {
        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();

        ColorAtlas.InitializeColors();

        SODataHandler.VerifyScriptableObjects();

        //UIManager.Instance.Initialize();

        Debug.Log(SceneNames.MAIN_MENU + " initialized!");
        EventBroadcaster.Instance.PostEvent(EventKeys.MENU_START, null);

    }

}
