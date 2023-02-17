using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerObserver: IObserver
{
    public void OnPlayerHit(Player player, Projectile proj);
}
