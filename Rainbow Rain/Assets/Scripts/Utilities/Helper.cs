using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{

    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if (_camera == null)
                _camera = Camera.main;
            return _camera;
        }
    }

}

public static class PlayerHelper
{
    private static Player _player;
    public static Player  Player
    {
        get
        {
            hasPlayerRef();
            return _player;
        }
    }
    public static Vector3 PlayerLocation
    {
        get
        {
            hasPlayerRef();
            return _player.transform.position;
        }
    }
    public static Color PlayerColor
    {
        get 
        {
            hasPlayerRef(); 
            return _player.PlayerColor; 
        }
    }
    private static void hasPlayerRef()
    {
        if (_player is null)
            initPlayerRef();
        else
            return;
    }
    private static void initPlayerRef()
    {
        _player = GameObject.FindGameObjectWithTag(TagNames.PLAYER).GetComponent<Player>();
    }
}


public static class BootstrapHelper
{
    public static void InitializeHandler(GameObject gameobjectParent)
    {
        if (gameobjectParent.GetComponent<Handler>() is null)
            return;

        //Debug.Log("found handler!");
        InitializeHandler(gameobjectParent.GetComponent<Handler>());
    }

    public static void InitializeHandler(Handler handler)
    {
        handler.Initialize();
    }

}
