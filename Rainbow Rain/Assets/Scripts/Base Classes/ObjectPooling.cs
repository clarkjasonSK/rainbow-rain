using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPooling : MonoBehaviour
{
    private ObjectPool<GameObject> _obj_pool;
    public ObjectPool<GameObject> GameObjectPool
    {
        get { return _obj_pool; }
    }

    [SerializeField] private GameObject ObjectTemplate;
    [SerializeField] private Transform ObjectTransform;

    [SerializeField] private bool CollectionCheck = false;
    [SerializeField] private int InitialPoolSize = 35;
    [SerializeField] private int MaxPoolSize = 150;

    [SerializeField] private bool instantiateOnAwake = true;

    void Awake()
    {
        if (instantiateOnAwake)
        {
            startPooling();
        }
    }

    public void startPooling()
    {
        _obj_pool = new ObjectPool<GameObject>(generateObject, onGetObject, onReleaseObject, onDestroyObject, CollectionCheck, InitialPoolSize, MaxPoolSize);
    }

    public GameObject generateObject()
    {
        GameObject tempObject = Instantiate(ObjectTemplate, ObjectTransform);
        tempObject.SetActive(false);
        Poolable tempPoolable = tempObject.GetComponent<Poolable>();

        if (tempPoolable != null)
        {
            tempPoolable.OnInstantiate();
            tempPoolable.SetObjectPool(this);
        }

        return tempObject;
    }

    public void onGetObject(GameObject gameObj)
    {
        gameObj.SetActive(true);

        Poolable tempPoolable = gameObj.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnActivate();
        }
    }

    public void onReleaseObject(GameObject gameObj)
    {
        gameObj.SetActive(false);
        Poolable tempPoolable = gameObj.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnDeactivate();
        }

    }

    public void onReturnObjects(List<GameObject> gameObjs)
    {
        foreach (GameObject go in gameObjs)
        {
            _obj_pool.Release(go);
        }
    }

    public void onDestroyObject(GameObject gameObj)
    {
        Destroy(gameObj);
        //Debug.Log("GameObject cast to the shadow realm");
    }
}
