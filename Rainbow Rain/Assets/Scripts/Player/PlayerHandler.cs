using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Handler
{
    #region Player References
    private Player _player;

    #endregion

    #region Event Variables
    Projectile projReference = null;
    #endregion

    public override void Initialize()
    {
        _player = PlayerHelper.Player;

        AddEventObservers();
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
    

    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
    }
    public void OnPlayerHit(EventParameters param)
    {
        //Player tempPlayer = param.GetParameter<Player>(EventParamKeys.playerParam, null);

        projReference = param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null);
        projReference.ProjectileActive = false;

        if (compareColors(_player.PlayerColor, projReference.ProjectileColor))
        {
            _player.absorbToSoul();
        }
        else
        {
            _player.damageToShell();
        }
        /*
        switch (GameManager.Instance.GameState)
        {
            case GameState.MAIN_MENU:
                _player.setPlayerColor(projReference.ProjectileColor);
                break;
            case GameState.INGAME:
                if (compareColors(_player.PlayerColor, projReference.ProjectileColor))
                {
                    _player.absorbToSoul();
                }
                else
                {
                    _player.damageToShell();
                }
                break;
        }*/

        EventBroadcaster.Instance.PostEvent(EventKeys.PROJ_DESPAWN, param);

        //ProjectileHandler.Instance.removeProjectile(projReference);
    }
}
