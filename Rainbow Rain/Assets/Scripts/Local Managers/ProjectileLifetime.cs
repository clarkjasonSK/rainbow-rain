using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : Singleton<ProjectileLifetime>, ISingleton
{

    [SerializeField] private string filename;
    private List<ProjectileInfo> _projectile_types;

    private List<float> _projectile_spawn_times;
    private List<float> _projectile_elapsed_times;

    [SerializeField] private float FastSpawnRate = .5f; // type 1 proj spawns per every value based in time.deltatime
    [SerializeField] private float NormalSpawnRate = 1f; // type 2 proj spawns per every value based in time.deltatime
    [SerializeField] private float SlowSpawnRate = 1.5f; // type 3 proj spawns per every value based in time.deltatime

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {

        _projectile_types = ProjectileJSONLoader.loadJSONInfo<ProjectileInfo>(filename, false);

        //_projectile_types = new List<ProjectileInfo>();
        //_projectile_types.Add(new ProjectileInfo(1, "SLOW", "LEFT", "END_BOUNDS", "STRAIGHT", 1, 2, "PLAYER", 1,3));

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
        isDone = true;
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

    public void deactivateProjectile(Projectile projectile)
    {
        ProjectileManager.Instance.removeProjectile(projectile);
        ProjectileObjectPool.Instance.returnDeactivateObject(projectile.gameObject);
    }
}
