using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ProjectileHelper
{
    private static Transform _proj_spawn_bound_x; // TEMPORARY
    private static Transform _proj_spawn_bound_y; // TEMPORARY

    public static void Initialize(Transform boundX, Transform boundY, float cameraOrthoSize)
    {
        _proj_spawn_bound_x = boundX;
        _proj_spawn_bound_y = boundY;
        _proj_spawn_bound_y.position = new Vector2(0, -1f * (cameraOrthoSize + .5f));
    }

    public static Vector2 getProjectileSpawn(string projSpawn)
    {
        Vector2 tempDirection = Vector2.zero;
        if (projSpawn == ProjDirection.RANDOM)
        {
            tempDirection = getRandomDirection();
        }
        else// SPECIFIC DIRECTION
        {
            tempDirection = getSpecificDirection(projSpawn);
        }

        if (tempDirection.x == 0) // up or down
        {
            tempDirection = new Vector2(Random.Range(_proj_spawn_bound_x.position.x + 1f, -1f * _proj_spawn_bound_x.position.x - 1f), -1f * _proj_spawn_bound_y.position.y * tempDirection.y);
        }
        else if (tempDirection.y == 0) // left or right
        {
            tempDirection = new Vector2(-1f * _proj_spawn_bound_x.position.x * tempDirection.x, Random.Range(_proj_spawn_bound_y.position.y + 1f, -1f * _proj_spawn_bound_y.position.y - 1f));
        }

        return tempDirection;
    }
    public static Vector2 getRandomDirection()
    {
        switch (Random.Range(1, 5))
        {
            case 1: // from up, going down
                return Vector2.up;
            case 2: // from down, going up
                return Vector2.down;
            case 3: // from left, going right
                return Vector2.left;
            case 4: // from right, going left
                return Vector2.right;
        }
        return Vector2.zero;
    }
    public static Vector2 getSpecificDirection(string direction)
    {
        switch (direction)
        {
            case ProjDirection.UP: return Vector2.up; // from up, going down
            case ProjDirection.DOWN: return Vector2.down; // from down, going up
            case ProjDirection.LEFT: return Vector2.left; // from left, going right
        }
        return Vector2.right; // from right, going left
    }

    public static Quaternion getProjectileRotation(string projTarget, Vector2 projLocation)
    {
        if (projTarget == ProjTarget.PLAYER)
        {
            return Quaternion.Euler(0, 0, getAngle(PlayerHelper.PlayerLocation, projLocation));
        }

        else if (projTarget == ProjTarget.END_BOUNDS)
        {
            if (projLocation.x == _proj_spawn_bound_x.position.x)
            {
                return Quaternion.Euler(0, 0, 0);
            }
            if (projLocation.y == _proj_spawn_bound_y.position.y)
            {
                return Quaternion.Euler(0, 0, 90);
            }
            if (projLocation.x == -_proj_spawn_bound_x.position.x)
            {
                return Quaternion.Euler(0, 0, 180);
            }
            /*if (projLocation.y == -ProjectileSpawnBoundY.position.y)
            {
                return Quaternion.Euler(0, 0, -90);
            }*/
            return Quaternion.Euler(0, 0, -90);
        }
        else
        {
            return Quaternion.identity;
        }
    }
    public static Quaternion getProjectileRotation(Vector2 playerLocation, Vector2 projLocation)
    {
        return Quaternion.Euler(0, 0, getAngle(playerLocation, projLocation));
    }
    private static float getAngle(Vector2 targetDirection, Vector2 projTransform)
    {
        return Mathf.Atan2((targetDirection - projTransform).y,
                            (targetDirection - projTransform).x) * Mathf.Rad2Deg;
    }

    public static float getProjectileSpawnRate(string projSpawn)
    {
        switch (projSpawn)
        {
            case ProjSpawnRate.FAST:
                return ProjSpawnRate.FAST_RATE;
            case ProjSpawnRate.NORMAL:
                return ProjSpawnRate.NORMAL_RATE;
            case ProjSpawnRate.SLOW:
                return ProjSpawnRate.SLOW_RATE;
        }
        return 0.0f;
    }
    public static Color getProjectileColor(string projColor)
    {
        if (projColor == ProjColor.RANDOM)
        {
            return ColorAtlas.getRandomColor();
        }
        if (projColor == ProjColor.PLAYER)
        {
            return PlayerHelper.PlayerColor;
        }

        return ColorAtlas.getRandomColor(PlayerHelper.PlayerColor);
    }
}
