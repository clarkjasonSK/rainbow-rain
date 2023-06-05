using System;

[Serializable]
public class ProjectileData : GameData
{

    public string ProjectileSpawnRate;
    public string ProjectileSpawnPosition;

    public string ProjectileTarget;
    public string ProjectilePath;
    public int ProjectileMinSpeed;
    public int ProjectileMaxSpeed;

    public string ProjectileColor;
    public int ProjectileMinSize;
    public int ProjectileMaxSize;
}