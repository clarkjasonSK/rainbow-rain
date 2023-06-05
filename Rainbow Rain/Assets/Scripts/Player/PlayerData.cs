using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{ 
    #region PlayerData Variables 

    private int _total_shell_health;
    public int TotalShellHealth
    {
        get { return this._total_shell_health; }
        set { this._total_shell_health = value; }
    }

    private int _current_shell_health;
    public int CurrentShellHealth
    {
        get { return this._current_shell_health; }
        set { this._current_shell_health = value; }
    }

    private Color _soul_color;
    public Color PlayerSoulColor
    {
        get { return this._soul_color; }
        set { this._soul_color = value; }
    }
    private Color _curent_color;
    public Color CurrentPlayerColor
    {
        get { return this._curent_color; }
        set { this._curent_color = value; }
    }
    public float SoulAlpha
    {
        get { return this._curent_color.a; }
    }

    private float _move_speed;
    public float MoveSpeed
    {
        get { return this._move_speed; }
        set { this._move_speed = value; }
    }
    #endregion

    #region PlayerData Methods
    public PlayerData(int shellHealth, float moveSpeed)
    {
        _total_shell_health = shellHealth;
        _current_shell_health = shellHealth;
        _move_speed = moveSpeed;
    }


    public void resetPlayer()
    {
        _current_shell_health = _total_shell_health;
        _curent_color = _soul_color - new Color(0,0,0,1);

    }
    public void increaseAlpha(float alphaValue)
    {
        this._curent_color += new Color(0, 0, 0, alphaValue);
    }


    #endregion
}
