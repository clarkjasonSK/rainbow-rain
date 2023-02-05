using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    //private List<Projectile> _projectile_list;

    [SerializeField] private Transform ProjectileSpawnBoundX;
    [SerializeField] private Transform ProjectileSpawnBoundY;
    [SerializeField] private float Type1SpawnRate =1; // type 1 proj spawns per every value based in time.deltatime
    [SerializeField] private float Type2SpawnRate = 1; // type 2 proj spawns per every value based in time.deltatime
    [SerializeField] private float Type3SpawnRate = 1; // type 3 proj spawns per every value based in time.deltatime

    private float type1SpawnTime, type2SpawnTime, type3SpawnTime;

    private Projectile _temp_projectile;
    private Vector2 tempTargetDirection;

    void Start()
    {
        //projectileList = new List<Projectile>();
        ProjectileSpawnBoundY.position = new Vector2(0, -1f*(Camera.main.orthographicSize+.5f));

    }

    void Update()
    {
        
       /* if (type1SpawnTime >= Type1SpawnRate)
        {
            tempTargetDirection = getRandomSpawnDirection();
            spawnSingleProj(1, Random.Range(1, 4), tempTargetDirection, 
                Vector2.Scale(tempTargetDirection, new Vector2(-1,-1)));
            type1SpawnTime = 0;
        }
        type1SpawnTime += Time.deltaTime;

        

        if (type2SpawnTime >= Type2SpawnRate)
        {
            spawnSingleProj( 2, Random.Range(2,4), getRandomSpawnDirection(), GameManager.Instance.getCurrentPlayerLocation());
            type2SpawnTime = 0;
        }
        type2SpawnTime += Time.deltaTime;
 */
        if (type3SpawnTime >= Type3SpawnRate)
        {
            spawnSingleProj(3, 1, getRandomSpawnDirection(), GameManager.Instance.getCurrentPlayerLocation());
            type3SpawnTime = 0;
        }
        type3SpawnTime += Time.deltaTime;

    }


    public void spawnSingleProj(int projType, int projSpeed, Vector2 spawnDirection, Vector2 targetDirection)
    {
        try
        {
            _temp_projectile = ProjectileObjectPool.Instance.getActivateObject().GetComponent<Projectile>();

            if (spawnDirection == Vector2.up || spawnDirection == Vector2.down)
            {
                _temp_projectile.onInit(projType, projSpeed, getRandomColor(), targetDirection,
                    new Vector2(Random.Range(ProjectileSpawnBoundX.position.x + 1f, -1f * ProjectileSpawnBoundX.position.x - 1f), -1f * ProjectileSpawnBoundY.position.y * spawnDirection.y));
            }
            else if (spawnDirection == Vector2.left || spawnDirection == Vector2.right)
            {
                _temp_projectile.onInit(projType, projSpeed, getRandomColor(), targetDirection,
                    new Vector2(-1f * ProjectileSpawnBoundX.position.x * spawnDirection.x, Random.Range(ProjectileSpawnBoundY.position.y + 1f, -1f * ProjectileSpawnBoundY.position.y - 1f)));
            }

        }
        catch
        {
            Debug.Log("No More Projectiles Available");
        }


    }

    public void deactivateProjectile(GameObject projectile)
    {
        ProjectileObjectPool.Instance.returnDeactivateObject(projectile);
    }

    private Vector2 getRandomSpawnDirection()
    {
        switch (Random.Range(1, 5))
        {
            case 1: return Vector2.up; // from up, going down
            case 2: return Vector2.down; // from down, going up
            case 3: return Vector2.left; // from left, going right
        }
        return Vector2.right; // from right, going left
    }

    private Color getRandomColor()
    {
        switch (Random.Range(1, 4))
        {
            case 1: return new Color(.5f, 1, 1, 1);
            case 2: return new Color(1, .5f, 1, 1);
        }
        return new Color(1, 1, .5f, 1);
    }
}
