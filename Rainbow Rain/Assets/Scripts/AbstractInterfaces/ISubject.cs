using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject
{
    void AddObserver(IObserver obsrvr);
    void RemoveObserver(IObserver obsrvr);
    void NotifyObservers();
}
