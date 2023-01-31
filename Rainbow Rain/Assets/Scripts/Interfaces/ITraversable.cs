using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITraversable
{
    public void Move(Vector2 inputs, float moveSpeed);
}
