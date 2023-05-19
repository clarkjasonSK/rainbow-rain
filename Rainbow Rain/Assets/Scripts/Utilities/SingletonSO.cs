using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 Inherit from this base class to create a singleton.
 e.g. public class MyClassName : SingletonSO<MyClassName>{}
*/
public abstract class SingletonSO<T> : SingletonSO where T : ScriptableObject, IInitializable, IEventObserver //abstract singleton with generic T of constraint type ScriptableObject
{
    private static T _instance; // local instance reference
    public static T Instance
    {
        get
        {
            if (_instance != null)
                return _instance; // possible condition 1; instantly returns

            return _instance = ScriptableObjectHelper.GetSOManager<T>(FileNames.SO_SINGLETONS + typeof(T).ToString()); // possible condition 2; finds assets in Resources/ScriptableObjects/Singletons, assigns to and returns _instance
        }
    }
    public abstract void Initialize();
    public virtual void AddEventObservers() { }

}

public abstract class SingletonSO : ScriptableObject{}
