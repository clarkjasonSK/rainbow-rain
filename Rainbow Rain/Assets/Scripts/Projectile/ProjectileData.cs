using UnityEngine;

public class ProjectileData : MonoBehaviour
{
    private int _type_id;
    public int ProjectileTypeID
    {
        get { return this._type_id; }
    }

    private string _path;
    public string ProjectilePath
    {
        get { return this._path; }
    }
    private float _speed;
    public float ProjectileSpeed
    {
        get { return this._speed; }
    }

    private Color _color;
    public Color ProjectileColor
    {
        get { return this._color; }
    }

    private float _current_duration;
    public float ProjectileCurrentDuration
    {
        get { return this._current_duration; }
        set { this._current_duration+=value; }
    }

    private float _total_duration;
    public float ProjectileTotalDuration
    {
        get { return this._total_duration; }
    }

    private bool _active = false;
    public bool ProjectileActive
    {
        get { return this._active; }
        set { this._active = value; }
    }
    
    public void Initialize(ProjJSONData jsonData, float speedMultiplier, float homingDuration)
    {
        _type_id = jsonData.DataID;
        _path = jsonData.ProjectilePath;
        _speed = Random.Range(jsonData.ProjectileMinSpeed, jsonData.ProjectileMaxSpeed + 1) * speedMultiplier;
        _color = ProjectileHelper.getProjectileColor(jsonData.ProjectileColor);

        if (jsonData.ProjectilePath == ProjPath.HOMING)
        {
            _current_duration = 0;
            _total_duration = homingDuration;
        }

    }

    public void resetProjectile()
    {
        _type_id = 0;
        _path = "";
        _speed = 0;

        _current_duration = 0;
        _total_duration = 0;

        _active = false;
    }

}
