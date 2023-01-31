using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Poolable : MonoBehaviour
{
    [SerializeField] protected ObjectPool poolOrigin;
    public void SetObjectPool(ObjectPool pool)
    {
        poolOrigin = pool;
    }
    public ObjectPool GetObjectPool()
    {
        return this.poolOrigin;
    }

    public abstract void OnInstantiate();
    public abstract void OnActivate();
    public abstract void OnDeactivate();
}
