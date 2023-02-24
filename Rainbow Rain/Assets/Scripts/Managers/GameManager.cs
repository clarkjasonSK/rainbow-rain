using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, ISingleton
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Temporary state variables
    private bool _main_menu = false;
    public bool AtMainMenu
    {
        get { return _main_menu; }
        set { _main_menu = value; }
    }

    private bool _game_active = true;
    public bool IsGameActive
    {
        get { return _game_active; }
        set { _game_active = value; }
    }
    #endregion

    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
    }

    #region Event Variables
    Projectile projReference = null;
    #endregion

    public void Initialize()
    {
        _player_reference = GameObject.FindWithTag(TagNames.PLAYER).GetComponent<Player>();

        //_player_reference.AddObserver(this);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);

        InputManager.Instance.toggleInputAllow(false);
        isDone = true;
    }

    void Update()
    {
        
    }

    public Vector3 getPlayerLocation()
    {
        return _player_reference.transform.position;

    }
    public Color getPlayerColor()
    {
        return _player_reference.PlayerColor;
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

    #region Event Broadcaster Notifications

    public void OnPlayerHit(EventParameters param)
    {
        /*
        if (_main_menu)
        {
            player.setPlayerColor(proj.ProjectileColor);
            return;
        }*/

        //Player tempPlayer = param.GetParameter<Player>(EventParamKeys.playerParam, null);

        projReference = param.GetParameter<Projectile>(EventParamKeys.projParam, null);
        projReference.ProjectileActive = false;

        if (compareColors(_player_reference.PlayerColor, projReference.ProjectileColor))
        {
            _player_reference.absorbToSoul();
        }
        else
        {
            _player_reference.damageToShell();
        }

        ProjectileManager.Instance.removeProjectile(projReference);
    }
    #endregion
}
