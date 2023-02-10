using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : Singleton<ProjectileManager>
{
    private List<Projectile> _projectile_list;

    void Awake()
    {
        _projectile_list = new List<Projectile>();
    }


    void Update()
    { 
        foreach(Projectile proj in _projectile_list)
        {
            proj.moveProjectile();
        }
    }

    public void addProjectile(Projectile proj)
    {
        _projectile_list.Add(proj);
    }

    

    
}
