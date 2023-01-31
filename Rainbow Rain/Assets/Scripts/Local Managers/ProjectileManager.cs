using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    // private List<Projectile> projectileList;
    private Projectile tempProjectile;

    [SerializeField] float projectileSpawnRate=1; // box spawns every value in seconds
    private float timePassed;

    Color tempColor;
    int tempRange;
    void Start()
    {
        //projectileList = new List<Projectile>()s;
        timePassed = projectileSpawnRate;

    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed >= projectileSpawnRate)
        {
            createProjectile();
            timePassed = 0;
        }
        timePassed += Time.deltaTime;
    }


    public void createProjectile()
    {
        try
        {
            tempProjectile = ProjectileObjectPool.Instance.getActivateObject().GetComponent<Projectile>();

            switch (Random.Range(1, 4))
            {
                case 1:
                    tempProjectile.onInit(new Color(.5f, 1, 1, 1), Random.Range(1, 4));
                    break;
                case 2:
                    tempProjectile.onInit(new Color(1, .5f, 1, 1), Random.Range(1, 4));
                    break;
                case 3:
                    tempProjectile.onInit(new Color(1, 1, .5f, 1), Random.Range(1, 4));
                    break;
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
}
