using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : Poolable
{
    #region Projectile Variables
    private ProjectileData _proj_data;
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
    private EventParameters _proj_event;
    #endregion

    #region Temporary Game Values
    [SerializeField] private float speedMultiplier = 4f;
    [SerializeField] private float homingDuration = 6f;
    [SerializeField] private float smallestSize = .4f;
    [SerializeField] private float sizeMultiplier = .2f;
    #endregion

    public void initProj(ProjJSONData projData)
    {
        _proj_data.Initialize(projData, speedMultiplier, homingDuration);
        _proj_controller.Initialize(projData, smallestSize, sizeMultiplier);

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
                _proj_event.AddParameter(EventParamKeys.PROJ_PARAM, this);
            }
            this.transform.rotation = ProjectileHelper.getProjectileRotation(PlayerHelper.PlayerLocation, this.transform.position);

            _proj_data.ProjectileCurrentDuration += Time.deltaTime;
        }
        _proj_controller.moveProjectile(_proj_data.ProjectileSpeed);
    }

    #region Poolable Functions
    public override void OnInstantiate()
    {
        if (this._proj_data is null)
            this._proj_data = GetComponent<ProjectileData>();
        if (this._proj_controller is null)
            this._proj_controller = GetComponent<ProjectileController>();

        _proj_event = new EventParameters();
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
            _proj_event.AddParameter(EventParamKeys.PROJ_PARAM, this);
            EventBroadcaster.Instance.PostEvent(EventKeys.PROJ_DESPAWN, _proj_event);
            //ProjectileManager.Instance.removeProjectile(this.gameObject); // TEMPORARY
        }
    }
}
