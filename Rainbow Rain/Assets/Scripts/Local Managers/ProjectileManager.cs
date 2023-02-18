using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>, ISingleton, IProjectileObserver
{
    private List<Projectile> _projectile_list;
    private ProjectileLifetime _proj_lifetime;

    public ProjectileLifetime ProjLifetime
    {
        get { return _proj_lifetime; }
    }
    private ProjectileUtilities _proj_utilities;
    public ProjectileUtilities ProjUtilities
    {
        get { return _proj_utilities; }
    }

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }


    public void Initialize()
    {
        _projectile_list = new List<Projectile>();
        _proj_lifetime = this.GetComponentInChildren<ProjectileLifetime>();
        _proj_utilities = this.GetComponentInChildren<ProjectileUtilities>();
        _proj_lifetime.initialize();
        _proj_utilities.initialize();
        isDone = true;
    }

    void Update()
    { 
        foreach(Projectile proj in _projectile_list.ToArray())
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

    #region Observer Functions
    public void OnNotify()
    {
        ;
    }
    public void OnProjectileExit(Projectile proj)
    {
        removeProjectile(proj);
    }
    #endregion
}
