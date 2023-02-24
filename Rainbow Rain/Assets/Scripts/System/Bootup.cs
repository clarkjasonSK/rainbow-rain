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

    }

}
