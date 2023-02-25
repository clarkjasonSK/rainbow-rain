using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootup : MonoBehaviour
{
    void Start()
    {
        EventBroadcaster.Instance.Initialize();
        GameManager.Instance.Initialize();

        InputManager.Instance.Initialize();
        ProjectileManager.Instance.Initialize();
        ColorDictionary.InitializeColors();

        if(EventBroadcaster.Instance.IsDoneInitializing &&
            GameManager.Instance.IsDoneInitializing &&
            InputManager.Instance.IsDoneInitializing &&
            ProjectileManager.Instance.IsDoneInitializing)
        {
            EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
        }
    }

}
