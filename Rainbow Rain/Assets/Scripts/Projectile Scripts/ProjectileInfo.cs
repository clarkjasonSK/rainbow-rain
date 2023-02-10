using UnityEngine;

public class ProjectileInfo
{
    private int _id;
    public int ProjectileID
    {
        get { return this._id; }
    }

    private string _spawn_rate;
    public string ProjectileSpawnRate
    {
        get { return this._spawn_rate; }
    }

    private string _spawn_position;
    public string ProjectileSpawnPosition
    {
        get { return this._spawn_position; }
    }

    private string _target;
    public string ProjectileTarget
    {
        get { return this._target; }
    }
    private string _path;
    public string ProjectilePath
    {
        get { return this._path; }
    }

    private int _min_speed;
    public int ProjectileMinSpeed
    {
        get { return this._min_speed; }
    }
    private int _max_speed;
    public int ProjectileMaxSpeed
    {
        get { return this._max_speed; }
    }

    private string _color;
    public string ProjectileColor
    {
        get { return this._color; }
    }

    public ProjectileInfo(int projID, string projSpawnRate, string projSpawnPosition, string projTarget, string projPath, int projMinSpeed, int projMaxSpeed, string projColor)
    {
        _id = projID;
        _spawn_rate= projSpawnRate;
        _spawn_position = projSpawnPosition;
        _target = projTarget;
        _path = projPath;
        _min_speed = projMinSpeed;
        _max_speed = projMaxSpeed;
        _color = projColor;
    }

}
