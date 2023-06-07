using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler: Handler
{
    [SerializeField] private ProjectileLifetime _proj_lifetime;
    [SerializeField] private ProjRefs _proj_refs;
    [SerializeField] private List<Projectile> _projectile_list = new List<Projectile>();

    public override void Initialize()
    {
        _proj_lifetime.Initialize();

        ProjectileHelper.Initialize(_proj_refs.ProjectileSpawnBoundX, _proj_refs.ProjectileSpawnBoundY, Helper.Camera.orthographicSize);

        AddEventObservers();
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PROJ_SPAWN, OnProjectileSpawn);
        EventBroadcaster.Instance.AddObserver(EventKeys.PROJ_DESPAWN, OnProjectileDespawn);
    }
    void Update()
    {
        if (GameManager.Instance.GameState == GameState.PROGRAM_START ||
            GameManager.Instance.GameState == GameState.PAUSED )
            return;
        
        foreach (Projectile proj in _projectile_list.ToArray())
        {
            proj.moveProjectile();
        }
    }

    private void addProjectile(Projectile proj)
    {
        _projectile_list.Add(proj);
    }

    private void removeProjectile(Projectile proj)
    {
        _projectile_list.Remove(proj);
        _proj_lifetime.deactivateProjectile(proj.gameObject);

    }

    #region EventBroadcaster  Functions
    public void OnProjectileSpawn(EventParameters param)
    {
        addProjectile(param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null));
    }
    public void OnProjectileDespawn(EventParameters param)
    {
        removeProjectile(param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null));
    }
    #endregion
}
