using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>, ISingleton
{
    private List<Projectile> _projectile_list;

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        _projectile_list = new List<Projectile>();
        isDone = true;
    }

    void Update()
    { 
        foreach(Projectile proj in _projectile_list.ToArray())
        {
            proj.moveProjectile();
        }
    }

    public void addProjectile(Projectile projectile)
    {
        _projectile_list.Add(projectile);
    }

    public void removeProjectile(Projectile projectile)
    {
        _projectile_list.Remove(projectile);
    }
    

    
}
