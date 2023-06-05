using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneBootstrap : MonoBehaviour, IBootstrapper
{
    public Handler[] GameHandlers;
    public void Awake()
    {
        LoadScene();
    }
    public void LoadScene()
    {

        for (int i = 0; i < GameHandlers.Length; i++)
        {
            BootstrapHelper.InitializeHandler(GameHandlers[i]);
        }

        this.gameObject.SetActive(false);

        Debug.Log(SceneNames.GAME_SCENE + " initialized!");

        /*
        PlayerHandler.Instance.Initialize();
        InputHandler.Instance.Initialize();

        //LevelHandler.Instance.Initialize();
        ProjectileHandler.Instance.Initialize();*/

        //EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);

    }
}
