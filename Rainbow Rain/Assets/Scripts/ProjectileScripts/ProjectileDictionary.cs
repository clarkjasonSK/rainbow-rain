using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileDictionary
{
    public const string PROJECTILES_JSON = "Assets/JSON/Projectiles.JSON";
}

public static class ProjSpawnRate
{
    public const string SLOW = "SLOW";
    public const string NORMAL = "NORMAL";
    public const string FAST = "FAST";

    public const float SLOW_RATE = 1.5f;
    public const float NORMAL_RATE = 1f;
    public const float FAST_RATE = .5f;
}

public static class ProjDirection
{
    public const string RANDOM = "RANDOM";
    public const string UP = "UP";
    public const string DOWN = "DOWN";
    public const string LEFT = "LEFT";
    public const string RIGHT = "RIGHT";
}

public static class ProjTarget
{
    public const string END_BOUNDS = "END_BOUNDS";
    public const string PLAYER = "PLAYER";
}

public static class ProjPath
{
    public const string STRAIGHT = "STRAIGHT";
    public const string HOMING = "HOMING";
    public const string BOOMERANG = "BOOMERANG";
    public const string SIN = "SIN";
    public const string COS = "COS";
}

public static class ProjColor
{
    public const string PLAYER = "PLAYER";
    public const string RANDOM = "RANDOM";
    public const string RANDOM_NO_PLAYER = "RANDOM_NO_PLAYER";
}