using UnityEngine;

public class ProjectileTraits
{
    private int _type_id;
    public int ProjectileTypeID
    {
        get { return this._type_id; }
        set { this._type_id = value; }
    }

    private string _path;
    public string ProjectilePath
    {
        get { return this._path; }
        set { this._path = value; }
    }
    private float _speed;
    public float ProjectileSpeed
    {
        get { return this._speed; }
        set { this._speed = value; }
    }

    private Color _color;
    public Color ProjectileColor
    {
        get { return this._color; }
        set { this._color = value; }
    }

    private float _current_duration;
    public float ProjectileCurrentDuration
    {
        get { return this._current_duration; }
        set { this._current_duration = value; }
    }

    private float _total_duration;
    public float ProjectileTotalDuration
    {
        get { return this._total_duration; }
        set { this._total_duration = value; }
    }

    private bool _active= false;
    public bool ProjectileActive
    {
        get { return this._active; }
        set { this._active = value; }
    }
    
    public void resetProjectile()
    {
        _type_id = 0;
        _path = "";
        _speed = 0;
        _active = false;

    }

}
