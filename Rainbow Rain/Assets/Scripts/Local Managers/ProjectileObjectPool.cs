using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObjectPool :  ObjectPool, ISingleton
{
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        isDone = true;
    }

}
