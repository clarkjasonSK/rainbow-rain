using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLifetime : MonoBehaviour, IInitializable
{ 
    private List<ProjJSONData> _projectile_types;

    private List<float> _projectile_spawn_times = new List<float>();
    private List<float> _projectile_elapsed_times = new List<float>();

    [SerializeField] private ObjectPooling projObjPool;

    #region Event Parameters
    private EventParameters lifetimeParam;
    #endregion

    public void Initialize()
    {
        SetProjectileTypes(null);

        lifetimeParam = new EventParameters();

    }


    public void SetProjectileTypes(LevlJSONData lvlData)
    {
        //TEST LOADING OF PROJECTILES
        _projectile_types = JsonLoader.loadJsonData<ProjJSONData>(FileNames.PROJECTILES_JSON, false);

        // TEST PROJECTILES
        _projectile_spawn_times.Clear();
        _projectile_elapsed_times.Clear();

        foreach (ProjJSONData pi in _projectile_types)
        {
            _projectile_spawn_times.Add(ProjectileHelper.getProjectileSpawnRate(pi.ProjectileSpawnRate));
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
                    lifetimeParam.AddParameter<Projectile>(EventParamKeys.PROJ_PARAM, cloneProjectile(_projectile_types[i]));
                    EventBroadcaster.Instance.PostEvent(EventKeys.PROJ_SPAWN, lifetimeParam);
                    //ProjectileHandler.Instance.addProjectile(cloneProjectile(_projectile_types[i]));
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

    public Projectile cloneProjectile(ProjJSONData projData)
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
