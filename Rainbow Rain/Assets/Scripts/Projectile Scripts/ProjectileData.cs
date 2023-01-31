using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileData
{
    private float _move_speed;
    public float MoveSpeed
    {
        get { return this._move_speed; }
        set { this._move_speed = value; }
    }

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

}
