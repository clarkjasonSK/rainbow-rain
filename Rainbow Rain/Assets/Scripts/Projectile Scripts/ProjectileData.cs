using UnityEngine;

public class ProjectileData
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

    private bool _initialized;
    public bool ProjectileInitialized
    {
        get { return this._initialized; }
        set { this._initialized = value; }
    }
    
    public void resetProjectile()
    {
        _type_id = 0;
        _path = "";
        _speed = 0;
        _initialized = false;

    }

}
