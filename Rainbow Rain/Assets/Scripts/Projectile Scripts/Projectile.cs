using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{

    private ProjectileData _proj_data;
    public ProjectileData ProjData
    {
        get { return _proj_data; }
    }
    private ProjectileController projController;


    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void placeProjectile(Vector2 position)
    {
        //projController.placeProjectile(position);
    }



    public void onInit(Color projColor, int projType)
    {
        _proj_data.ProjectileColor = projColor;
        _proj_data.ProjectileType = projType;
        projController.ProjectileColor = projColor;
        projController.ProjectileType = projType;

    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        this._proj_data = new ProjectileData();
        this.projController = GetComponent<ProjectileController>();
    }

    public override void OnActivate()
    {
        projController.placeProjectile( new Vector2( Random.Range(-4f, 4f), Random.Range(-8f, 8f)) );
        
    }

    public override void OnDeactivate()
    {
        //
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ProjectileManager.Instance.deactivateProjectile(this.gameObject);
    }
}
