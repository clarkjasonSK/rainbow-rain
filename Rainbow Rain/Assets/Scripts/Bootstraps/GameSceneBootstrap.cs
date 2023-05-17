using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour, IBootstrapper
{
    public void Awake()
    {
        LoadScene();
    }
    public void LoadScene()
    {
        PlayerHandler.Instance.Initialize();
        InputHandler.Instance.Initialize();

        LevelHandler.Instance.Initialize();
        ProjectileHandler.Instance.Initialize();

        if (PlayerHandler.Instance.IsDoneInitializing &&
            InputHandler.Instance.IsDoneInitializing &&
            LevelHandler.Instance.IsDoneInitializing &&
            ProjectileHandler.Instance.IsDoneInitializing)
        {
            Debug.Log(SceneNames.GAME_SCENE + " initialized!");
            //EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }
}
