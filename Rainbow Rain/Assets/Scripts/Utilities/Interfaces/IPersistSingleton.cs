using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistSingleton
{
    public bool IsDoneInitializing { get; }
    public void Initialize();
}
