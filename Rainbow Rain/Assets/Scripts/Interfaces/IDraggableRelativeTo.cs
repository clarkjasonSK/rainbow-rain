using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggableRelativeTo
{
    public void DragRelativeTo(Vector2 touchLocation, float moveSpeed);
}
