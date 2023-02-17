using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{

    [SerializeField] private string filename;
    private List<ProjectileInfo> _projectile_types;
    private ProjectileObjectPool projObjPool;

    private List<float> _projectile_spawn_times;
    private List<float> _projectile_elapsed_times;

    //spawns per every value based in time.deltatime
    [SerializeField] private float FastSpawnRate = .5f;
    [SerializeField] private float NormalSpawnRate = 1f;
    [SerializeField] private float SlowSpawnRate = 1.5f;

    public void initialize()
    {

        projObjPool = GetComponent<ProjectileObjectPool>();

        _projectile_types = ProjectileJSONLoader.loadJSONInfo<ProjectileInfo>(filename, false);

        //_projectile_types = new List<ProjectileInfo>();
        //_projectile_types.Add(new ProjectileInfo(1, "SLOW", "LEFT", "END_BOUNDS", "STRAIGHT", 1, 2, "PLAYER", 1,3));

        _projectile_spawn_times = new List<float>();
        _projectile_elapsed_times = new List<float>();

        foreach (ProjectileInfo pi in _projectile_types)
        {
            switch (pi.ProjectileSpawnRate)
            {
                case ProjSpawnRate.FAST:
                    _projectile_spawn_times.Add(FastSpawnRate);
                    break;
                case ProjSpawnRate.NORMAL:
                    _projectile_spawn_times.Add(NormalSpawnRate);
                    break;
                case ProjSpawnRate.SLOW:
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
        Projectile tempProjectile = projObjPool.GameObjectPool.Get().GetComponent<Projectile>();

        if (tempProjectile.ProjectileActive)
        {
            //Debug.Log("it's already in the scene bro");
            //return null;
        }
        tempProjectile.initProj(projInfo);

        return tempProjectile;
    }

    public void deactivateProjectile(GameObject projObj)
    {
        projObjPool.GameObjectPool.Release(projObj);
    }
}
