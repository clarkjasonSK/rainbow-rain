using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dictionary
{

}
public static class FileNames
{
    public const string SO_SINGLETONS = "ScriptableObjects/Singletons/";

    public const string ASSET_RESOURCES = "Assets/Resources/";
    public const string ASSET_EXTENSION = ".asset";

    public const string LEVELS_SO_PATH = "ScriptableObjects/Levels/";
    public const string PATTERNS_SO_PATH = "ScriptableObjects/Patterns/";
    public const string PROJECTILES_SO_PATH = "ScriptableObjects/Projectiles/";

    public const string CURRENT_LEVEL_SO = LEVELS_SO_PATH + "CurrentLevel";

    public const string SO_LIST = ASSET_RESOURCES + "JSON/SOList.JSON";

    public const string LEVELS_JSON = ASSET_RESOURCES + "JSON/Levels.JSON";
    public const string PATTERNS_JSON = ASSET_RESOURCES + "JSON/Patterns.JSON";
    public const string PROJECTILES_JSON = ASSET_RESOURCES + "JSON/Projectiles.JSON";

}
public class SceneNames
{
    public const string MAIN_MENU = "MainMenu";
    public const string GAME_SCENE = "GameScene";

}
public static class TagNames
{
    public const string PLAYER = "Player";

    public const string PROJECTILES_PARENT = "ProjectilesParent";
    public const string PROJECTILE_BOUNDS = "ProjectileBounds";
    public const string PROJECTILE = "Projectile";

    public const string MAIN_CAMERA = "MainCamera";

}

// ALL BELOW WILL BE REWORKED
public static class PlayerDictionary
{
    // 1 = Cyan; 2 = Magenta; 3 = Yellow
    public static int PlayerColor = 1;
    public static int ShellHealth = 3;
    public static float ShellStartAlpha = 1f;

    public static float MoveSpeed = 10f;


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