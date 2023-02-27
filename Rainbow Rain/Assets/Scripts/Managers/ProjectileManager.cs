using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>, ISingleton
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    private List<Projectile> _projectile_list = new List<Projectile>();

    [SerializeField] private Transform _projectiles_parent;
    public Transform ProjectilesParent
    {
        get { return _projectiles_parent; }
    }

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

    public void Initialize()
    {
        getProjectilesParent();

        _proj_lifetime = this.gameObject.AddComponent<ProjectileLifetime>();
        _proj_utilities = this.gameObject.AddComponent<ProjectileUtilities>();

        _proj_lifetime.initialize();
        _proj_utilities.initialize();

        EventBroadcaster.Instance.AddObserver(EventKeys.PROJ_DESPAWN, OnProjectileExit);

        isDone = true;
    }

    public void ReInitialize() 
    {
        _projectile_list.Clear();
        getProjectilesParent();

        _proj_lifetime.reinitialize();
        _proj_utilities.reinitialize();

    }

    private void getProjectilesParent()
    {
        Debug.Log("getting parent: " + _projectiles_parent);
        _projectiles_parent = GameObject.FindGameObjectWithTag(TagNames.PROJECTILES_PARENT).transform;
        Debug.Log("got parent: " + _projectiles_parent);
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
        removeProjectile(param.GetParameter<Projectile>(EventParamKeys.projParam, null));
    }
    #endregion
}
