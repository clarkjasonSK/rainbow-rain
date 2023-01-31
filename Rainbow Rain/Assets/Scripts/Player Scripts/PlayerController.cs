using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ITraversable, IDraggable
{
    [SerializeField] private SpriteRenderer playerSoulSprite;
    [SerializeField] private SpriteRenderer playerShellSprite;
    private Rigidbody2D playerRigidBody;
    private CircleCollider2D playerCircleCollider;
    public Color SpriteColor
    {
        get { return playerSoulSprite.color; }
    }
    /*
    private float moveSpeed { set; get; }
    public float MoveSpeed
    {
        get { return this.moveSpeed; }
        set { this.moveSpeed = value; }
    }*/
    //private Vector2 currPos;

    void Start()
    {
        //playerSoulSprite = GetComponent<SpriteRenderer>();
        //playerShellSprite = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {

    }

    public void initControllerComponents(Rigidbody2D rb, CircleCollider2D cc)
    {
       // playerSoulSprite = soulSprt;
       // playerShellSprite = shellSprt;
        playerRigidBody = rb;
        playerCircleCollider = cc;
    }
    public void initPlayerColor(Color soulColor, Color shellColor)
    {
        playerSoulSprite.color = soulColor;
        playerShellSprite.color = shellColor;

        //Debug.Log(soulColor + " and "+ shellColor);
        //Debug.Log(playerShellSprite.color);
    }

    public void setSoulColor(Color newSoulColor)
    {
        playerSoulSprite.color = newSoulColor;
        /*playerSoulSprite.color = new Color(playerSoulSprite.color.r, 
                                            playerSoulSprite.color.g, 
                                            playerSoulSprite.color.b, 
                                            alpha);*/
    }
    public void decreaseShellColor(float newShellColor)
    {
        playerShellSprite.color-= new Color(0,0,0, newShellColor);
    }

    public void Move(Vector2 inputs, float moveSpeed)
    {
        transform.position = new Vector2(transform.position.x + (inputs.x * Time.deltaTime * moveSpeed),
                                          transform.position.y + (inputs.y * Time.deltaTime * moveSpeed));
    }

    public void MoveAtPoint(Vector2 point)
    {
        transform.position = point;
    }

    public void destroyShell()
    {
        playerCircleCollider.radius = .3f;
    }
}
