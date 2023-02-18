using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class APlayerSubject : MonoBehaviour
{
    private protected List<IPlayerObserver> _player_observers = new List<IPlayerObserver>();
    public void AddObserver(IPlayerObserver obsrvr)
    {
        _player_observers.Add(obsrvr);
    }
    public void RemoveObserver(IPlayerObserver obsrvr)
    {
        _player_observers.Remove(obsrvr);
    }
    public void NotifyObservers()
    {
        _player_observers.ForEach(
            (obsrvr) =>
            {
                obsrvr.OnNotify();
            });
    }
    protected void NotifyPlayerHit(Player player, Projectile projectile)
    {
        _player_observers.ForEach(
            (obsrvr) =>
            {
                obsrvr.OnPlayerHit(player, projectile);
            });
    }
}
