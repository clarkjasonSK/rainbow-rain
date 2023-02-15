using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable
{
    public void Drag(Vector2 touchLocation, float moveSpeed);
}
