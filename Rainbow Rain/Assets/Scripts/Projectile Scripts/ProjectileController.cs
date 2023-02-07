using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private SpriteRenderer _proj_sprite; 
    public Color ProjectileColor 
    {
        set 
        {
            if (_proj_sprite == null)
            {
                _proj_sprite = GetComponent<SpriteRenderer>();
            }
            _proj_sprite.color = value; 
        } 
    }

    //[SerializeField] private Rigidbody2D _proj_rigidbody;
    //public Rigidbody2D ProjectileRigidbody { get { return _proj_rigidbody; } }

    private Animator _proj_animator;
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
    }

    /*
    private float moveSpeed;
    public float ProjectileMoveSpeed
    {
        get { return this.moveSpeed; }
        set { this.moveSpeed = value; }
    }*/

    private void Start()
    {
        _proj_sprite = GetComponent<SpriteRenderer>();
        _proj_animator = GetComponent<Animator>();

    }

    

    public void moveProjectile(Vector2 direction, float moveSpeed)
    {
        //ProjectileRigidbody.velocity = direction * moveSpeed ;

        this.transform.Translate(direction*Time.deltaTime*moveSpeed);

       /* this.transform.position = new Vector2(
            transform.position.x + direction.x * moveSpeed * Time.fixedDeltaTime,
            transform.position.y + direction.y * moveSpeed * Time.fixedDeltaTime);*/
    }
    private float getTargetAngle(Vector2 targetDirection)
    {
        Vector3 tempVector = new Vector3(targetDirection.x, targetDirection.y, 0) - this.transform.position;
        return Mathf.Atan2(tempVector.y, tempVector.x) * Mathf.Rad2Deg;
    }
    public void rotateProjectile(Vector2 targetDirection)
    {
        this.transform.rotation = Quaternion.Euler(0, 0, getTargetAngle(targetDirection));
    }
    public void placeProjectile(Vector2 position)
    {
        this.transform.position = position;
        //Debug.Log("spawned at: " + position);
    }
}
