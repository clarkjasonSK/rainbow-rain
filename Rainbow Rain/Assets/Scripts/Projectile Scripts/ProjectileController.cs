using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer projSprite;
    public Color ProjectileColor
    {
        get { return projSprite.color; }
        set { projSprite.color = value; }
    }
    private int projType;
    public int ProjectileType
    {
        set { projType = value; }
    }

    private float moveSpeed;
    public float ProjectileMoveSpeed
    {
        get { return this.moveSpeed; }
        set { this.moveSpeed = value; }
    }

    private void Start()
    {
        //projSprite = GetComponent<SpriteRenderer>();
    }


    public void placeProjectile(Vector2 position)
    {
        this.transform.position = position;
    }
}
