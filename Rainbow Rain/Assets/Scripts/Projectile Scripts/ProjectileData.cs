using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData
{

    private int _type;
    public int ProjectileType
    {
        get { return this._type; }
        set { this._type = value; }
    }

    private Color _color;
    public Color ProjectileColor
    {
        get { return this._color; }
        set { this._color = value; }
    }

    private float _move_speed;
    public float MoveSpeed
    {
        get { return this._move_speed; }
        set { this._move_speed = value; }
    }


    private Vector2 _target_direction;
    public Vector2 TargetDirection
    {
        get { return _target_direction; }
        set { _target_direction = value; }
    }

    private bool _initialized;
    public bool ProjectileInitialized
    {
        get { return this._initialized; }
        set { this._initialized = value; }
    }

    public void resetProjectile()
    {
        _type = 0;
        _move_speed = 0;
        _target_direction = Vector2.zero;
        _initialized = false;
    }


    /*
    private Vector2 _target_position;
    public Vector2 TargetPosition
    {
        get { return _target_position; }
        set { _target_position = value; }
    }*/
}
