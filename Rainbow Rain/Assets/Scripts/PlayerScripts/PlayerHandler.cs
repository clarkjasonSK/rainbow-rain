using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Singleton<PlayerHandler>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = true;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Player References
    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
    }
    public Vector3 PlayerLocation
    {
        get { return _player_reference.transform.position; }
    }
    public Color PlayerColor
    {
        get { return _player_reference.PlayerColor; }
    }
    #endregion

    #region Event Variables
    Projectile projReference = null;
    #endregion

    void Start()
    {
        if (_player_reference == null)
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        _player_reference = GameObject.FindGameObjectWithTag(TagNames.PLAYER).GetComponent<Player>();

        AddEventObservers();
        isDone = true;
    }

    public bool compareColors(Color playerColor, Color projColor)
    {
        if (playerColor.r == projColor.r &&
            playerColor.g == projColor.g &&
            playerColor.b == projColor.b)
        {
            return true;
        }
        return false;
    }
    

    private void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
    }
    public void OnPlayerHit(EventParameters param)
    {
        //Player tempPlayer = param.GetParameter<Player>(EventParamKeys.playerParam, null);

        projReference = param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null);
        projReference.ProjectileActive = false;

        switch (GameManager.Instance.GameState)
        {
            case GameState.MAIN_MENU:
                _player_reference.setPlayerColor(projReference.ProjectileColor);
                break;
            case GameState.INGAME:
                if (compareColors(_player_reference.PlayerColor, projReference.ProjectileColor))
                {
                    _player_reference.absorbToSoul();
                }
                else
                {
                    _player_reference.damageToShell();
                }
                break;
        }

        ProjectileHandler.Instance.removeProjectile(projReference);
    }
}
