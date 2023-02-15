using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, ISingleton
{
    private Player _player_instance;

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        _player_instance = GameObject.FindWithTag("Player").GetComponent<Player>();
        isDone = true;
    }

    void Update()
    {
        
    }

    public Vector3 getPlayerLocation()
    {
        return _player_instance.transform.position;

    }
    public Color getPlayerColor()
    {
        return _player_instance.getPlayerColor();
    }

    public bool compareColors(Color playerColor, Color projColor)
    {
        if (playerColor.r == projColor.r &&
            playerColor.g == projColor.g &&
            playerColor.b == projColor.b)
        {
            return true;
        }
        return false;
    }
}
