using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSceneBootstrap : MonoBehaviour, IBootstrapper
{
    public GameObject[] MainMenuHandlers;

    public void Awake()
    {
        LoadScene();
    }
    public void LoadScene()
    {
        for (int i = 0; i < MainMenuHandlers.Length; i++)
        {
            initializeHandler(MainMenuHandlers[i]);
        }

        this.gameObject.SetActive(false);
    }
    private void initializeHandler(GameObject gameobjectParent)
    {
        if (gameobjectParent.GetComponent<Handler>() is null)
            return;

        //Debug.Log("found handler!");
        gameobjectParent.GetComponent<Handler>().Initialize();
    }
}
