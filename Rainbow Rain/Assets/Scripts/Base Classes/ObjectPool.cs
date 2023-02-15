using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> activePool, inactivePool;
    [SerializeField] private GameObject objectTemplate;
    [SerializeField] private Transform objectTransform;

    public int poolSize;
    public bool instantiateOnAwake;

    void Awake()
    {
        if (instantiateOnAwake)
        {
            generatePools();
        }
    }

    public void generatePools()
    {
        activePool = new List<GameObject>();
        inactivePool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject tempObject = Instantiate(objectTemplate, objectTransform);
            tempObject.SetActive(false);

            inactivePool.Add(tempObject);

            Poolable tempPoolable = tempObject.GetComponent<Poolable>();

            if (tempPoolable != null)
            {
                tempPoolable.OnInstantiate();
                tempPoolable.SetObjectPool(this);
            }

        }
    }

    public GameObject getActivateObject()
    {
        if (inactivePool.Count == 0)
        {
            return null;
        }

        int i = -1; // TEMPORARY FIX DUE TO REPEATEDLY RETURNING AND USING THE SAME OBJECT FOR SOME REASON 
        do
        {
            i++;
        } while (inactivePool[i].activeInHierarchy && i<inactivePool.Count);
        GameObject tempObject = inactivePool[i];

        tempObject.SetActive(true);
        activePool.Add(tempObject);
        inactivePool.Remove(tempObject);

        Poolable tempPoolable = tempObject.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnActivate();
        }

        return tempObject;
    }

    public void returnDeactivateObject(GameObject tempObject)
    {
        if (!activePool.Contains(tempObject))
            return;

        Poolable tempPoolable = tempObject.GetComponent<Poolable>();
        if (tempPoolable != null)
        {
            tempPoolable.OnDeactivate();

        }
        tempObject.SetActive(false);

        inactivePool.Add(tempObject);
        activePool.Remove(tempObject);

    }

    public void returnDeactivateObject(List<GameObject> tempObjects)
    {
        foreach (GameObject po in tempObjects)
            returnDeactivateObject(po);
    }
}
