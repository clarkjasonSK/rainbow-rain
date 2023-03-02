using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Singleton<PlayerHandler>
{
    #region Player References
    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
        set { _player_reference = value; }
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

    #region Event Variables
    Projectile projReference = null;
    #endregion

    public void Start()
    {
        AddEventObservers();
    }

    private void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
    }
    public void OnPlayerHit(EventParameters param)
    {
        //Player tempPlayer = param.GetParameter<Player>(EventParamKeys.playerParam, null);

        projReference = param.GetParameter<Projectile>(EventParamKeys.projParam, null);
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
