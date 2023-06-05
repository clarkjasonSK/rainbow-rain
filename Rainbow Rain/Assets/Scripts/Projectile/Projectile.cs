using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{
    #region Projectile Variables
    private ProjectileTraits _proj_data;
    private ProjectileController _proj_controller;
    public bool ProjectileActive
    {
        get { return _proj_data.ProjectileActive; }
        set { _proj_data.ProjectileActive = value; }
    }
    public Color ProjectileColor
    {
        get { return _proj_data.ProjectileColor; }
    }
    #endregion

    #region Event Parameters
    private EventParameters projectileDespawn;
    #endregion

    #region Temporary Game Values
    [SerializeField] private float speedMultiplier = 4f;
    [SerializeField] private float homingDuration = 6f;
    [SerializeField] private float smallestSize = .4f;
    [SerializeField] private float sizeMultiplier = .2f;
    #endregion

    public void initProj(ProjectileData projData)
    {
        
        _proj_data.ProjectileTypeID = projData.DataID;
        _proj_data.ProjectilePath = projData.ProjectilePath;
        _proj_data.ProjectileSpeed = Random.Range(projData.ProjectileMinSpeed, projData.ProjectileMaxSpeed + 1)* speedMultiplier;

        _proj_data.ProjectileColor = ProjectileHandler.Instance.ProjUtilities.getProjectileColor(projData.ProjectileColor);
        _proj_controller.ProjectileColor = _proj_data.ProjectileColor;

        float tempSize = Random.Range(projData.ProjectileMinSize-1, projData.ProjectileMaxSize);
        transform.localScale = new Vector3( smallestSize+ (sizeMultiplier* tempSize), smallestSize+ (sizeMultiplier * tempSize), 1);

        if(_proj_data.ProjectilePath == ProjPath.HOMING)
        {
            _proj_data.ProjectileTotalDuration = homingDuration;
            _proj_data.ProjectileCurrentDuration = 0;
        }

        _proj_controller.placeProjectile(ProjectileHandler.Instance.ProjUtilities.getProjectileSpawn(projData.ProjectileSpawnPosition));
        transform.rotation = ProjectileHandler.Instance.ProjUtilities.getProjectileRotation(projData.ProjectileTarget, this.transform.position);



        _proj_data.ProjectileActive = true;


    }

    public void moveProjectile()
    {
        if (!_proj_data.ProjectileActive)
        {
            return;
        }

        if (_proj_data.ProjectilePath == ProjPath.HOMING)
        {
            if (_proj_data.ProjectileCurrentDuration >= _proj_data.ProjectileTotalDuration)
            {
                projectileDespawn.AddParameter(EventParamKeys.PROJ_PARAM, this);
            }
            this.transform.rotation = ProjectileHandler.Instance.ProjUtilities.getProjectileRotation(PlayerHelper.PlayerLocation, this.transform.position);
            _proj_data.ProjectileCurrentDuration += Time.deltaTime;
        }
        _proj_controller.moveProjectile(_proj_data.ProjectileSpeed);
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        this._proj_controller = GetComponent<ProjectileController>();
        _proj_data = new ProjectileTraits();
        projectileDespawn = new EventParameters();
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
        /*if (collision.CompareTag(TagNames.PLAYER))
        {
            //_proj_data.ProjectileActive = false;
            //ProjectileManager.Instance.removeProjectile(this.gameObject); // TEMPORARY
            //Debug.Log("projectile collision first");
        }*/
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE_BOUNDS) && _proj_data.ProjectileActive)
        {
            //NotifyProjectileExit(this);
            projectileDespawn.AddParameter(EventParamKeys.PROJ_PARAM, this);
            EventBroadcaster.Instance.PostEvent(EventKeys.PROJ_DESPAWN, projectileDespawn);
            //ProjectileManager.Instance.removeProjectile(this.gameObject); // TEMPORARY
        }
    }
}
