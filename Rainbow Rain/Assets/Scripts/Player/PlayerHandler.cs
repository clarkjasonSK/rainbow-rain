using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Handler
{
    #region Player References
    private Player _player;

    #endregion

    #region Event Variables
    Projectile projReference;
    #endregion

    public override void Initialize()
    {
        _player = PlayerHelper.Player;

        AddEventObservers();
    }

    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
    }

    public void OnPlayerHit(EventParameters param)
    {
        projReference = param.GetParameter<Projectile>(EventParamKeys.PROJ_PARAM, null);
        projReference.ProjectileActive = false;  // TO BE REMOVED

        if (compareColors(_player.PlayerColor, projReference.ProjectileColor))
        {
            _player.AbsorbToSoul();
            Debug.Log("absorb1");
        }
        else
        {
            _player.DamageToShell();
            Debug.Log("damage");
        }

        EventBroadcaster.Instance.PostEvent(EventKeys.PROJ_DESPAWN, param);

        //ProjectileHandler.Instance.removeProjectile(projReference);
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

}
