using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>, ISingleton, IProjectileObserver
{
    private Transform _projectiles_parent;
    public Transform ProjectilesParent
    {
        get { return _projectiles_parent; }
    }
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
        _projectiles_parent = GameObject.FindGameObjectWithTag(TagNames.PROJECTILES_PARENT).transform;

        _proj_lifetime = this.gameObject.AddComponent<ProjectileLifetime>();
        _proj_utilities = this.gameObject.AddComponent<ProjectileUtilities>();

        _proj_lifetime.initialize();
        _proj_utilities.initialize();
        isDone = true;
    }

    void Update()
    {
        /*
        if (!GameManager.Instance.IsGameActive)
            return;
        if (!GameManager.Instance.AtMainMenu)
            return;
        */


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
