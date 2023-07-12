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

    public Color PlayerAlphaIncrement = new Color(0,0,0,.10f);

    public float PlayerCursorOffset;

    public float ProjSpeedMultiplier = 4f;
    public float ProjHomingDuration = 6f;
    public float ProjSmallestSize = .4f;
    public float ProjSizeMultiplier = .2f;

}
