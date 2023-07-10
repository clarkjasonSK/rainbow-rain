using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile")]
public class ProjectileScriptableObject : GameScriptableObject
{
    #region Projectile Values

    [SerializeField] private int _id;
    public int ProjectileID
    {
        get { return _id; }
    }

    [SerializeField] private string _name;
    public string ProjectileName
    {
        get { return _name; }
    }

    [SerializeField] private string _spawn_rate;
    public string ProjectileSpawnRate
    {
        get { return _spawn_rate; }
    }

    [SerializeField] private string _spawn_position;
    public string ProjectileSpawnPosition
    {
        get { return _spawn_position; }
    }

    [SerializeField] private string _target;
    public string ProjectileTarget
    {
        get { return _target; }
    }

    [SerializeField] private string _path;
    public string ProjectilePath
    {
        get { return _path; }
    }

    [SerializeField] private int _min_speed;
    public int ProjectileMinSpeed
    {
        get { return _min_speed; }
    }

    [SerializeField] private int _max_speed;
    public int ProjectileMaxSpeed
    {
        get { return _max_speed; }
    }

    [SerializeField] private string _color;
    public string ProjectileColor
    {
        get { return _color; }
    }

    [SerializeField] private int _min_size;
    public int ProjectileMinSize
    {
        get { return _min_size; }
    }

    [SerializeField] private int _max_size;
    public int ProjectileMaxSize
    {
        get { return _max_size; }
    }

    #endregion

    public override void InstantiateData<TData>(TData JSONData)
    {
        assignValues(JSONData as ProjJSONData);
    }
    public void assignValues(ProjJSONData projData)
    {
        _id = projData.DataID;
        _name = projData.DataName;

        _spawn_rate = projData.ProjectileSpawnRate;
        _spawn_position = projData.ProjectileSpawnPosition;

        _target = projData.ProjectileTarget;
        _path = projData.ProjectilePath;
        _min_speed = projData.ProjectileMinSpeed;
        _max_speed = projData.ProjectileMaxSpeed;

        _color = projData.ProjectileColor;
        _min_size = projData.ProjectileMinSize;
        _max_size = projData.ProjectileMaxSize;
    }
}
