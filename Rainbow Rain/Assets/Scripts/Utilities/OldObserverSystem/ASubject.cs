using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASubject : MonoBehaviour
{
    private protected List<IObserver> _observers = new List<IObserver>();
    public void AddObserver(IObserver obsrvr)
    {
        _observers.Add(obsrvr);
    }
    public void RemoveObserver(IObserver obsrvr)
    {
        _observers.Remove(obsrvr);
    }
    protected void NotifyObservers()
    {
        _observers.ForEach(
            (_observer) => 
            { 
                _observer.OnNotify(); 
            });
    }
}
