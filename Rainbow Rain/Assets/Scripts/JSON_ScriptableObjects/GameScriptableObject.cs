using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameScriptableObject : ScriptableObject
{
    public abstract void InstantiateData<TData>(TData JSONData) where TData : JSONData;

}
