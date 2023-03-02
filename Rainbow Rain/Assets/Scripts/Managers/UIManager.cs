using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>, IPersistSingleton
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion




    public void Initialize()
    {

        isDone = true;
    }

    public void goToGame()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.START_GAME, null);
    }

}
