using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player _player_instance;
    void Start()
    {
        _player_instance = GameObject.FindWithTag("Player").GetComponent<Player>();
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
