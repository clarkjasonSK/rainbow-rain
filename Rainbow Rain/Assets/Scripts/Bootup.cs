using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootup : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.Initialize();
        InputManager.Instance.Initialize();
        ProjectileManager.Instance.Initialize();
        ProjectileLifetime.Instance.Initialize();
        ProjectileUtilities.Instance.Initialize();
        //ProjectileObjectPool.Instance.Initialize();

    }

}
