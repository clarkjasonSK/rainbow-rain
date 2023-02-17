using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AProjectileSubject : Poolable
{
    private protected List<IProjectileObserver> _proj_observers = new List<IProjectileObserver>();
    public void AddObserver(IProjectileObserver obsrvr)
    {
        _proj_observers.Add(obsrvr);
    }
    public void RemoveObserver(IProjectileObserver obsrvr)
    {
        _proj_observers.Remove(obsrvr);
    }
    public void NotifyObservers()
    {
        _proj_observers.ForEach(
            (obsrvr) =>
            {
                obsrvr.OnNotify();
            });
    }
    protected void NotifyProjectileExit(Projectile proj)
    {
        _proj_observers.ForEach(
            (obsrvr) =>
            {
                obsrvr.OnProjectileExit(proj);
            });
    }
}
