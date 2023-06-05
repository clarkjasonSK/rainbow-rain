using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{ 
    private List<ProjectileData> _projectile_types;

    private List<float> _projectile_spawn_times = new List<float>();
    private List<float> _projectile_elapsed_times = new List<float>();

    [SerializeField] private ObjectPooling projObjPool;


    public void initialize()
    {

        _projectile_types = JsonLoader.loadJsonData<ProjectileData>(FileNames.PROJECTILES_JSON, false);

        //_projectile_types = new List<ProjectileInfo>();
        //_projectile_types.Add(new ProjectileInfo(1, "SLOW", "LEFT", "END_BOUNDS", "STRAIGHT", 1, 2, "PLAYER", 1,3));

       // Debug.Log("PojectileHandler instance: " + ProjectileHandler.Instance);

        foreach (ProjectileData pi in _projectile_types)
        {
            _projectile_spawn_times.Add(ProjectileHandler.Instance.ProjUtilities.getProjectileSpawnRate(pi.ProjectileSpawnRate));
            _projectile_elapsed_times.Add(0);
        }

    }


    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED)
            return;

        for (int i = 0; i < _projectile_types.Count; i++)
        {
            if (_projectile_elapsed_times[i] >= _projectile_spawn_times[i])
            {
                try
                {
                    ProjectileHandler.Instance.addProjectile(cloneProjectile(_projectile_types[i]));
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

    public Projectile cloneProjectile(ProjectileData projData)
    {
        Projectile tempProjectile = projObjPool.GameObjectPool.Get().GetComponent<Projectile>();

        if (tempProjectile.ProjectileActive)
        {
            //Debug.Log("it's already in the scene bro");
            //return null;
        }
        tempProjectile.initProj(projData);

        return tempProjectile;
    }

    public void deactivateProjectile(GameObject projObj)
    {
        projObjPool.GameObjectPool.Release(projObj);
    }
}