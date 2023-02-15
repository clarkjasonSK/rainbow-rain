using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileUtilities : Singleton<ProjectileUtilities>, ISingleton
{
    private List<Color> _projectile_colors;

    [SerializeField] private Transform ProjectileSpawnBoundX;
    [SerializeField] private Transform ProjectileSpawnBoundY;

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        ProjectileSpawnBoundY.position = new Vector2(0, -1f * (Camera.main.orthographicSize + .5f));

        _projectile_colors = new List<Color>();
        _projectile_colors.Add(new Color(.5f, 1, 1, 1));
        _projectile_colors.Add(new Color(1, .5f, 1, 1));
        _projectile_colors.Add(new Color(1, 1, .5f, 1));
        Debug.Log("Color population: " + _projectile_colors.Count);
        isDone = true;
    }

    public Vector2 getProjectileSpawn(string projSpawn)
    {
        Vector2 tempDirection = Vector2.zero;
        if (projSpawn == "RANDOM")
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
            case "UP": return Vector2.up; // from up, going down
            case "DOWN": return Vector2.down; // from down, going up
            case "LEFT": return Vector2.left; // from left, going right
        }
        return Vector2.right; // from right, going left
    }

    public Quaternion getProjectileRotation(string projTarget, Vector2 projLocation)
    {
        if (projTarget == "PLAYER")
        {
            return Quaternion.Euler(0, 0, getAngle(GameManager.Instance.getPlayerLocation(), projLocation));
        }
        //projTarget == "END_BOUNDS"

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
    public Quaternion getProjectileRotation(Vector2 playerLocation, Vector2 projLocation)
    {
        return Quaternion.Euler(0, 0, getAngle(playerLocation, projLocation));
    }
    private float getAngle(Vector2 targetDirection, Vector2 projTransform)
    {
        return Mathf.Atan2((targetDirection - projTransform).y,
                            (targetDirection - projTransform).x) * Mathf.Rad2Deg;
    }

    public Color getProjectileColor(string projColor)
    {
        if (projColor == "RANDOM")
        {
            return _projectile_colors[Random.Range(0, _projectile_colors.Count)];
        }
        if (projColor == "PLAYER")
        {
            return (GameManager.Instance.getPlayerColor() + new Color(0, 0, 0, 1f));
        }

        //RANDOM_NO_PLAYER
        Color excemptColor = GameManager.Instance.getPlayerColor();

        /*
        _projectile_colors.Remove(excemptColor);

        Color tempColor = _projectile_colors[Random.Range(1, _projectile_colors.Count + 1)];
        _projectile_colors.Add(excemptColor);
        */

        // pick your poison way of doing this lmao

        Color tempColor;
        do
        {
            tempColor = _projectile_colors[Random.Range(0, _projectile_colors.Count)];

        } while (tempColor.r == excemptColor.r && tempColor.g == excemptColor.g && tempColor.b == excemptColor.b);
        return (tempColor + new Color(0, 0, 0, 1f));
    }
}
