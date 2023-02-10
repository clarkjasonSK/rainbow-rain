using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{

    private ProjectileData _proj_data;
    private ProjectileController _proj_controller;
    private float _speed_multiplier = 4;
    private void Start()
    {

        if (_proj_data == null || _proj_controller == null)
        {
            //this._proj_data = new ProjectileData();
            this._proj_controller = GetComponent<ProjectileController>();
        }
    }
    void Update()
    {

        

    }
    public void moveProjectile()
    {
        if (!_proj_data.ProjectileInitialized)
        {
            return;
        }

        if(_proj_data.ProjectilePath == "HOMING")
        {
            this.transform.rotation = ProjectileUtilities.Instance.getProjectileRotation(GameManager.Instance.getPlayerLocation(), this.transform.position);
        }
        _proj_controller.moveProjectile(_proj_data.ProjectileSpeed);
    }

    public void initProj(ProjectileInfo projInfo)
    {
        _proj_data = new ProjectileData();
        _proj_data.ProjectileTypeID = projInfo.ProjectileID;
        _proj_data.ProjectilePath = projInfo.ProjectilePath;
        _proj_data.ProjectileSpeed = Random.Range(projInfo.ProjectileMinSpeed, projInfo.ProjectileMaxSpeed + 1)* _speed_multiplier;
        _proj_data.ProjectileColor = ProjectileUtilities.Instance.getProjectileColor(projInfo.ProjectileColor);
        _proj_controller.ProjectileColor = _proj_data.ProjectileColor;


        _proj_controller.placeProjectile(ProjectileUtilities.Instance.getProjectileSpawn(projInfo.ProjectileSpawnPosition));
        transform.rotation = ProjectileUtilities.Instance.getProjectileRotation(projInfo.ProjectileTarget, this.transform.position);

        _proj_data.ProjectileInitialized = true;


    }
    /*
    public void onInit(int projType, int projSpeed, Color projColor, Vector2 targetDirection, Vector2 spawnPosition)
    {
        _proj_data.ProjectileType = projType;
        _proj_data.ProjectileColor = projColor;
        _proj_controller.ProjectileColor = projColor;

        _proj_data.ProjectileSpeed = projSpeed * 4f; ///////////////////revise
        _proj_controller.placeProjectile(spawnPosition);
        _proj_data.TargetDirection = targetDirection;

        if (_proj_data.ProjectileType !=1)
        {
            _proj_controller.ProjectileAnimator.enabled = false;

            _proj_controller.rotateProjectile(targetDirection);

            _proj_data.TargetDirection = Vector2.right;
        }

        _proj_data.ProjectileInitialized = true;
    }*/

    public Color getProjectileColor()
    {
        return _proj_data.ProjectileColor;
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        //this._proj_data = new ProjectileData();
        this._proj_controller = GetComponent<ProjectileController>();
    }

    public override void OnActivate()
    {
    }

    public override void OnDeactivate()
    {
        _proj_data.resetProjectile();
    }
    #endregion
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProjectileLifetime.Instance.deactivateProjectile(this.gameObject);
            //Debug.Log("hit player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileBounds"))
        {
            ProjectileLifetime.Instance.deactivateProjectile(this.gameObject);
        }
    }
}
