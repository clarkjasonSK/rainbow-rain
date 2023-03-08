using System;
using System.Collections.Generic;

[Serializable]
public class PatternData
{
    public int PatternID;
    public bool PatternRepteatable;
    public float PatternDuration;
    public List<int> PatternProjectiles;
}
/*
[Serializable]
public class PatternProjectile
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
}

*/
