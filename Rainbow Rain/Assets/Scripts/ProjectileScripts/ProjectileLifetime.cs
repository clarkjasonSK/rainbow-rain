using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour
{

    private string filename = ProjectileDictionary.PROJECTILES_JSON;
    private List<ProjectileInfo> _projectile_types;
    private ObjectPooling projObjPool;

    private List<float> _projectile_spawn_times = new List<float>();
    private List<float> _projectile_elapsed_times = new List<float>();

    //spawns per every value based in time.deltatime

    public void initialize()
    {
        projObjPool = this.gameObject.AddComponent<ObjectPooling>();
        getObjPoolChildren();

        _projectile_types = ProjectileJSONLoader.loadJSONInfo<ProjectileInfo>(filename, false);

        //_projectile_types = new List<ProjectileInfo>();
        //_projectile_types.Add(new ProjectileInfo(1, "SLOW", "LEFT", "END_BOUNDS", "STRAIGHT", 1, 2, "PLAYER", 1,3));

        populateTimeLists();

    }

    public void reinitialize()
    {
        _projectile_spawn_times.Clear();
        _projectile_elapsed_times.Clear();
        getObjPoolChildren();
        populateTimeLists();
    }

    public void getObjPoolChildren()
    {
        projObjPool.ObjectTemplate = ProjectileManager.Instance.ProjectilesParent.GetChild(0).gameObject;
        projObjPool.ObjectTransform = ProjectileManager.Instance.ProjectilesParent.GetChild(4);
    }
    private void populateTimeLists()
    {
        foreach (ProjectileInfo pi in _projectile_types)
        {
            _projectile_spawn_times.Add(ProjectileManager.Instance.ProjUtilities.getProjectileSpawnRate(pi.ProjectileSpawnRate));
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
