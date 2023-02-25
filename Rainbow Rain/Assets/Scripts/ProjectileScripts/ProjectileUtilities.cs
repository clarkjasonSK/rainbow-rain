using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUtilities : MonoBehaviour
{

    [SerializeField] private Transform ProjectileSpawnBoundX;
    [SerializeField] private Transform ProjectileSpawnBoundY;

    public void initialize()
    {
        ProjectileSpawnBoundX = ProjectileManager.Instance.ProjectilesParent.GetChild(2).transform;
        ProjectileSpawnBoundY = ProjectileManager.Instance.ProjectilesParent.GetChild(3).transform;

        ProjectileSpawnBoundY.position = new Vector2(0, -1f * (Camera.main.orthographicSize + .5f));

    }

    public Vector2 getProjectileSpawn(string projSpawn)
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
            tempDirection = new Vector2(Random.Range(ProjectileSpawnBoundX.position.x + 1f, -1f * ProjectileSpawnBoundX.position.x - 1f), -1f * ProjectileSpawnBoundY.position.y * tempDirection.y);
        }
        else if (tempDirection.y == 0) // left or right
        {
            tempDirection = new Vector2(-1f * ProjectileSpawnBoundX.position.x * tempDirection.x, Random.Range(ProjectileSpawnBoundY.position.y + 1f, -1f * ProjectileSpawnBoundY.position.y - 1f));
        }

        return tempDirection;
    }
    public Vector2 getRandomDirection()
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
    public Vector2 getSpecificDirection(string direction)
    {
        switch (direction)
        {
            case ProjDirection.UP: return Vector2.up; // from up, going down
            case ProjDirection.DOWN: return Vector2.down; // from down, going up
            case ProjDirection.LEFT: return Vector2.left; // from left, going right
        }
        return Vector2.right; // from right, going left
    }

    public Quaternion getProjectileRotation(string projTarget, Vector2 projLocation)
    {
        if (projTarget == ProjTarget.PLAYER)
        {
            return Quaternion.Euler(0, 0, getAngle(GameManager.Instance.PlayerLocation, projLocation));
        }

        else if(projTarget == ProjTarget.END_BOUNDS)
        {
            if (projLocation.x == ProjectileSpawnBoundX.position.x)
            {
                return Quaternion.Euler(0, 0, 0);
            }
            if (projLocation.y == ProjectileSpawnBoundY.position.y)
            {
                return Quaternion.Euler(0, 0, 90);
            }
            if (projLocation.x == -ProjectileSpawnBoundX.position.x)
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
    public Quaternion getProjectileRotation(Vector2 playerLocation, Vector2 projLocation)
    {
        return Quaternion.Euler(0, 0, getAngle(playerLocation, projLocation));
    }
    private float getAngle(Vector2 targetDirection, Vector2 projTransform)
    {
        return Mathf.Atan2((targetDirection - projTransform).y,
                            (targetDirection - projTransform).x) * Mathf.Rad2Deg;
    }

    public float getProjectileSpawnRate(string projSpawn)
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
    public Color getProjectileColor(string projColor)
    {
        if (projColor == ProjColor.RANDOM)
        {
            return ColorDictionary.getRandomColor();
        }
        if (projColor == ProjColor.PLAYER)
        {
            return GameManager.Instance.PlayerColor;
        }

        return ColorDictionary.getRandomColor(GameManager.Instance.PlayerColor);
    }
}
