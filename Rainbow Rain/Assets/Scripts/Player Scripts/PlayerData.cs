using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
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

    private Color _color;
    public Color PlayerColor
    {
        get { return this._color; }
        set { this._color = value; }
    }
    public float SoulAlpha
    {
        get { return this._color.a; }
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
        _color = new Color(_color.r, _color.g, _color.b, 0);

    }
    public void increaseAlpha(float alphaValue)
    {
        this._color += new Color(0, 0, 0, alphaValue);
    }


    #endregion
}
