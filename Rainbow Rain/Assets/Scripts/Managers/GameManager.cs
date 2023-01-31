using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public bool compareColors(Color playerColor, Color projColor)
    {
        if(playerColor.r == projColor.r &&
            playerColor.g == projColor.g &&
            playerColor.b == projColor.b)
        {
            return true;
        }
        return false;
    }
}
