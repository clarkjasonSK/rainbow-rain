using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _proj_sprite; 

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

    public void Initialize(ProjJSONData jsonData, float smallestSize, float sizeMultiplier)
    {
        _proj_sprite.color = ProjectileHelper.getProjectileColor(jsonData.ProjectileColor);


        float tempSize = Random.Range(jsonData.ProjectileMinSize - 1, jsonData.ProjectileMaxSize);
        transform.localScale = new Vector3(smallestSize + (sizeMultiplier * tempSize), smallestSize + (sizeMultiplier * tempSize), 1);

        placeProjectile(ProjectileHelper.getProjectileSpawn(jsonData.ProjectileSpawnPosition));

        transform.rotation = ProjectileHelper.getProjectileRotation(jsonData.ProjectileTarget, this.transform.position);

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
