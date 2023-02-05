using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{

    private ProjectileData _proj_data;
    public ProjectileData ProjData { get { return _proj_data; }}
    private ProjectileController _proj_controller;

    private void Start()
    {
        if (_proj_data == null || _proj_controller == null)
        {
            this._proj_data = new ProjectileData();
            this._proj_controller = GetComponent<ProjectileController>();
        }
    }
    void Update()
    {
        /*
        if (_proj_data.ProjectileInitialized)
        {
            return;
        }*/
        /*
        switch (_proj_data.ProjectileType)
        {
            case 1: 

                break;
            case 2:
                
                break;
            case 3:

                break;
        }*/
        if(_proj_data.ProjectileType == 3)
        {
            _proj_controller.rotateProjectile(GameManager.Instance.getCurrentPlayerLocation());
        }

        _proj_controller.moveProjectile(_proj_data.TargetDirection, _proj_data.MoveSpeed);

        
    }

    

    public void onInit(int projType, int projSpeed, Color projColor, Vector2 targetDirection, Vector2 spawnPosition)
    {
        _proj_data.ProjectileType = projType;
        _proj_data.ProjectileColor = projColor;
        _proj_controller.ProjectileColor = projColor;

        _proj_data.MoveSpeed = projSpeed * 4f; ///////////////////revise
        _proj_controller.placeProjectile(spawnPosition);
        _proj_data.TargetDirection = targetDirection;

        if (_proj_data.ProjectileType !=1)
        {
            _proj_controller.ProjectileAnimator.enabled = false;

            
            _proj_controller.rotateProjectile(targetDirection);

            _proj_data.TargetDirection = transform.right;
        }

        _proj_data.ProjectileInitialized = true;
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        this._proj_data = new ProjectileData();
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
            ProjectileManager.Instance.deactivateProjectile(this.gameObject);
            //Debug.Log("hit player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileBounds"))
        {
            ProjectileManager.Instance.deactivateProjectile(this.gameObject);
        }
    }
}
