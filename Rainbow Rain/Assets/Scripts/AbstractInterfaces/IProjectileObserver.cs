using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectileObserver : IObserver
{
    public void OnProjectileExit(Projectile proj);
}
