using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/Settings/GameSettings")]
public class GameSettings : ScriptableObject
{

    [Range(1, 3)] public int PlayerColor = 1;
    public int PlayerShellHealth = 3;
    public float PlayerShellStartAlpha = 1f;

    public float PlayerMoveSpeed = 10f;
}
