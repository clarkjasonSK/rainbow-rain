using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHandler: Singleton<ProjectileHandler>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = true;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    private List<Projectile> _projectile_list = new List<Projectile>();

    [SerializeField] private ProjectileLifetime _proj_lifetime;
    public ProjectileLifetime ProjLifetime
    {
        get { return _proj_lifetime; }
    }
    [SerializeField] private ProjectileUtilities _proj_utilities;
    public ProjectileUtilities ProjUtilities
    {
        get { return _proj_utilities; }
    }
    private void Start()
    {
        if(_proj_utilities == null || _proj_lifetime == null)
            Initialize();
    }
    public void Initialize()
    {
        _proj_utilities.initialize();
        _proj_lifetime.initialize();

        EventBroadcaster.Instance.AddObserver(EventKeys.PROJ_DESPAWN, OnProjectileExit);

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

    public void addProjectile(Projectile proj)
    {
        _projectile_list.Add(proj);
    }

    public void removeProjectile(Projectile proj)
    {
        _projectile_list.Remove(proj);
        _proj_lifetime.deactivateProjectile(proj.gameObject);

    }

    #region EventBroadcaster  Functions
    public void OnProjectileExit(EventParameters param)
    {
        removeProjectile(param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null));
    }
    #endregion
}
