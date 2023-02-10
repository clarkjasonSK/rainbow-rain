using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : Singleton<ProjectileLifetime>
{

    private List<ProjectileInfo> _projectile_types;

    private List<float> _projectile_spawn_times;
    private List<float> _projectile_elapsed_times;

    [SerializeField] private float FastSpawnRate = .5f; // type 1 proj spawns per every value based in time.deltatime
    [SerializeField] private float NormalSpawnRate = 1f; // type 2 proj spawns per every value based in time.deltatime
    [SerializeField] private float SlowSpawnRate = 1.5f; // type 3 proj spawns per every value based in time.deltatime

    void Awake()
    {
        //// TEMPORARY DATA INIT
        _projectile_types = new List<ProjectileInfo>();
        _projectile_types.Add(new ProjectileInfo(1, "SLOW", "LEFT", "END_BOUNDS", "STRAIGHT", 3, 3, "PLAYER"));
        _projectile_types.Add(new ProjectileInfo(1, "SLOW", "RIGHT", "END_BOUNDS", "STRAIGHT", 3, 3, "PLAYER"));
        _projectile_types.Add(new ProjectileInfo(2, "FAST", "RANDOM", "PLAYER", "STRAIGHT", 2, 3, "RANDOM_NO_PLAYER"));
        _projectile_types.Add(new ProjectileInfo(3, "SLOW", "RANDOM", "PLAYER", "HOMING", 1, 2, "RANDOM_NO_PLAYER"));
        Debug.Log("Projectile Types: " + _projectile_types.Count);


        //// TEMPORARY DATA INIT

        _projectile_spawn_times = new List<float>();
        _projectile_elapsed_times = new List<float>();

        foreach (ProjectileInfo pi in _projectile_types)
        {
            switch (pi.ProjectileSpawnRate)
            {
                case "FAST":
                    _projectile_spawn_times.Add(FastSpawnRate);
                    break;
                case "NORMAL":
                    _projectile_spawn_times.Add(NormalSpawnRate);
                    break;
                case "SLOW":
                    _projectile_spawn_times.Add(SlowSpawnRate);
                    break;
            }
            _projectile_elapsed_times.Add(0);
        }
    }

    void Update()
    {
        for (int i = 0; i < _projectile_types.Count; i++)
        {
            if (_projectile_elapsed_times[i] >= _projectile_spawn_times[i])
            {
                try
                {
                    ProjectileManager.Instance.addProjectile(cloneProjectile(_projectile_types[i]));
                }
                catch
                {
                    Debug.Log("No More Projectiles Available");
                }
                _projectile_elapsed_times[i] = 0;
            }
            _projectile_elapsed_times[i] += Time.deltaTime;
        }
    }

    public Projectile cloneProjectile(ProjectileInfo projInfo)
    {
        Projectile tempProjectile = ProjectileObjectPool.Instance.getActivateObject().GetComponent<Projectile>();
        tempProjectile.initProj(projInfo);

        return tempProjectile;
    }

    public void deactivateProjectile(GameObject projectile)
    {
        ProjectileObjectPool.Instance.returnDeactivateObject(projectile);
    }
}
