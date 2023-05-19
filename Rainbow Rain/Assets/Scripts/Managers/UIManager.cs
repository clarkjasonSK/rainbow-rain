using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "ScriptableObjects/Singletons/UIManager")]
public class UIManager : SingletonSO<UIManager>, IInitializable, IEventObserver
{

    public override void Initialize()
    {
        AddEventObservers();

    }

    public override void AddEventObservers()
    {

    }

    public void StartGame()
    {
        EventBroadcaster.Instance.PostEvent(EventKeys.GAME_START, null);
    }

}
