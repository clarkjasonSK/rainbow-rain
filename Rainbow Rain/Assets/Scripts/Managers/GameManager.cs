using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>, ISingleton, IPlayerObserver
{
    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
    }

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

    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        _player_reference = GameObject.FindWithTag(TagNames.PLAYER).GetComponent<Player>();
        _player_reference.AddObserver(this);
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

    #region Observer Notifications
    public void OnNotify()
    {
        ;
    }

    public void OnPlayerHit(Player player, Projectile proj)
    {
        /*
        if (_main_menu)
        {
            player.setPlayerColor(proj.ProjectileColor);
            return;
        }*/

        proj.ProjectileActive = false;
        if (compareColors(player.PlayerColor, proj.ProjectileColor))
        {
            player.absorbToSoul();
        }
        else
        {
            player.damageToShell();
        }
        ProjectileManager.Instance.removeProjectile(proj);
    }
    #endregion
}
