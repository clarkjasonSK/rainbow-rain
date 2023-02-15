using System;

[Serializable]
public class ProjectileInfo
{
    public int ProjectileID;

    public string ProjectileSpawnRate;
    public string ProjectileSpawnPosition;

    public string ProjectileTarget;
    public string ProjectilePath;
    public int ProjectileMinSpeed;
    public int ProjectileMaxSpeed;

    public string ProjectileColor;
    public int ProjectileMinSize;
    public int ProjectileMaxSize;

    public ProjectileInfo(int projID, string projSpawnRate, string projSpawnPosition, string projTarget, string projPath, int projMinSpeed, int projMaxSpeed, string projColor, int projMinSize, int projMaxSize)
    {
        ProjectileID = projID;
        ProjectileSpawnRate = projSpawnRate;
        ProjectileSpawnPosition = projSpawnPosition;
        ProjectileTarget = projTarget;
        ProjectilePath = projPath;
        ProjectileMinSpeed = projMinSpeed;
        ProjectileMaxSpeed = projMaxSpeed;
        ProjectileColor = projColor;
        ProjectileMinSize = projMinSize;
        ProjectileMaxSize = projMaxSize;
    }

}
