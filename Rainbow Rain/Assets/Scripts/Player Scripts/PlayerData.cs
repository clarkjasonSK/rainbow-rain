using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{ 
    #region PlayerData Variables 

    private int _total_lives;
    public int TotalLives
    {
        get { return this._total_lives; }
        set { this._total_lives = value; }
    }

    [SerializeField] private int _current_lives;
    public int CurrentLives
    {
        get { return this._current_lives; }
        set { this._current_lives = value; }
    }

    private Color _player_color;
    public Color PlayerColor
    {
        get { return this._player_color; }
        set { this._player_color = value; }
    }
    public float PlayerAlpha
    {
        get { return this._player_color.a; }
    }

    private float _move_speed;
    public float MoveSpeed
    {
        get { return this._move_speed; }
        set { this._move_speed = value; }
    }
    #endregion

    #region PlayerData Methods
    public PlayerData(int playerLives)
    {
        this._total_lives = playerLives;
        this._current_lives = playerLives;
        resetPlayer();
    }


    public void resetPlayer()
    {
        _current_lives = _total_lives;
        _player_color = new Color(_player_color.r, _player_color.g, _player_color.b, 0);

    }
    public void increaseAlpha(float alphaValue)
    {
        this._player_color += new Color(0, 0, 0, alphaValue);
    }


    #endregion
}
