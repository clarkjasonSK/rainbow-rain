using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _proj_sprite; 
    public Color ProjectileColor 
    {
        set 
        {
            _proj_sprite.color = value; 
        } 
    }

    //[SerializeField] private Rigidbody2D _proj_rigidbody;
    //public Rigidbody2D ProjectileRigidbody { get { return _proj_rigidbody; } }

    /*private Animator _proj_animator;
    public Animator ProjectileAnimator 
    { 
        get 
        {
            if (_proj_animator == null)
            {
                _proj_animator = GetComponent<Animator>();
            }
            return _proj_animator; 
        } 
    }*/

    private void Start()
    {
       // _proj_sprite = GetComponent<SpriteRenderer>();
        //_proj_animator = GetComponent<Animator>();
        //_proj_rigidbody = GetComponent<Rigidbody2D>();

    }

    public void moveProjectile(float moveSpeed)
    {
        //ProjectileRigidbody.velocity = direction * moveSpeed ;

        this.transform.Translate(Vector2.right*Time.deltaTime*moveSpeed);
    }

    public void placeProjectile(Vector2 position)
    {
        this.transform.position = position;
        //Debug.Log("spawned at: " + position);
    }
}
