using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, ITraversable, IDraggable
{
    [SerializeField] private SpriteRenderer _player_soul_sprite; 
    [SerializeField] private SpriteRenderer _player_shell_sprite;
    //private Rigidbody2D _player_rigidbody;
    private CircleCollider2D _player_circle_collider;

    public Color SpriteColor
    {
        get { return _player_soul_sprite.color; }
    }

    //private Vector2 currPos;

    void Start()
    {
        //_player_rigidbody = GetComponent<Rigidbody2D>();
        _player_circle_collider = GetComponent<CircleCollider2D>();

        this.transform.position = Vector2.zero;
    }


    public void setPlayerColor(Color soulColor, Color shellColor)
    {
        _player_soul_sprite.color = soulColor;
        _player_shell_sprite.color = shellColor;
    }

    public void setSoulColor(Color newSoulColor)
    {
        _player_soul_sprite.color = newSoulColor;

        /*playerSoulSprite.color = new Color(playerSoulSprite.color.r, 
                                            playerSoulSprite.color.g, 
                                            playerSoulSprite.color.b, 
                                            alpha);*/
    }
    public void decreaseShellColor(float newShellColor)
    {
        _player_shell_sprite.color-= new Color(0,0,0, newShellColor);
    }
    public void destroyShellCollider()
    {
        _player_circle_collider.radius = .3f; //////////////////////// to be revised
    }

    #region ITraversable
    public void Traverse(Vector2 inputs, float moveSpeed)
    {
        transform.position = new Vector2(transform.position.x + (inputs.x * Time.deltaTime* moveSpeed),
                                          transform.position.y + (inputs.y * Time.deltaTime * moveSpeed));
    }
    #endregion

    #region IDraggable
    public void Drag(Vector2 dragLocation, float moveSpeed)
    {
        this.transform.rotation = getPlayerRotation(dragLocation, 
            new Vector2 (this.transform.position.x, this.transform.position.y));

        this.transform.Translate(Vector2.right * Time.deltaTime * moveSpeed);
    }
    #endregion

    private Quaternion getPlayerRotation(Vector2 dragLocation, Vector2 playerLocation)
    {
        return Quaternion.Euler(0, 0, getAngle(dragLocation, playerLocation));
    }
    private float getAngle(Vector2 dragDirection, Vector2 playerLocation)
    {
        return Mathf.Atan2((dragDirection - playerLocation).y,
                            (dragDirection - playerLocation).x) * Mathf.Rad2Deg;
    }
}
