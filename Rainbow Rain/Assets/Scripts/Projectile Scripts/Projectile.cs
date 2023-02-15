using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{

    private ProjectileData _proj_data;
    private ProjectileController _proj_controller;

    //temporary variables
    private float speedMultiplier = 4f;
    private float homingDuration = 6f;
    private float smallestSize = .4f;
    private float sizeMultiplier = .2f;


    private void Start()
    {

        if (_proj_data == null || _proj_controller == null)
        {
            this._proj_controller = GetComponent<ProjectileController>();
        }
    }

    public void moveProjectile()
    {
        if (!_proj_data.ProjectileInitialized)
        {
            return;
        }

        if(_proj_data.ProjectilePath == "HOMING")
        {
            if(_proj_data.ProjectileCurrentDuration>= _proj_data.ProjectileTotalDuration)
            {
                ProjectileManager.Instance.ProjLifetime.deactivateProjectile(this); // TEMPORARY 
            }
            this.transform.rotation = ProjectileManager.Instance.ProjUtilities.getProjectileRotation(GameManager.Instance.getPlayerLocation(), this.transform.position);
            _proj_data.ProjectileCurrentDuration += Time.deltaTime;
        }
        _proj_controller.moveProjectile(_proj_data.ProjectileSpeed);
    }

    public void initProj(ProjectileInfo projInfo)
    {
        _proj_data = new ProjectileData();
        _proj_data.ProjectileTypeID = projInfo.ProjectileID;
        _proj_data.ProjectilePath = projInfo.ProjectilePath;
        _proj_data.ProjectileSpeed = Random.Range(projInfo.ProjectileMinSpeed, projInfo.ProjectileMaxSpeed + 1)* speedMultiplier;

        _proj_data.ProjectileColor = ProjectileManager.Instance.ProjUtilities.getProjectileColor(projInfo.ProjectileColor);
        _proj_controller.ProjectileColor = _proj_data.ProjectileColor;

        float tempSize = Random.Range(projInfo.ProjectileMinSize-1, projInfo.ProjectileMaxSize);
        transform.localScale = new Vector3( smallestSize+ (sizeMultiplier* tempSize), smallestSize+ (sizeMultiplier * tempSize), 1);

        if(_proj_data.ProjectilePath == "HOMING")
        {
            _proj_data.ProjectileTotalDuration = homingDuration;
            _proj_data.ProjectileCurrentDuration = 0;
        }

        _proj_controller.placeProjectile(ProjectileManager.Instance.ProjUtilities.getProjectileSpawn(projInfo.ProjectileSpawnPosition));
        transform.rotation = ProjectileManager.Instance.ProjUtilities.getProjectileRotation(projInfo.ProjectileTarget, this.transform.position);

        _proj_data.ProjectileInitialized = true;


    }
    public Color getProjectileColor()
    {
        return _proj_data.ProjectileColor;
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
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
        if (collision.CompareTag(TagNames.PLAYER))
        {
            ProjectileManager.Instance.ProjLifetime.deactivateProjectile(this); // TEMPORARY
            //Debug.Log("hit player");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE_BOUNDS))
        {
            ProjectileManager.Instance.ProjLifetime.deactivateProjectile(this); // TEMPORARY
        }
    }
}
