using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Box : MonoBehaviour
{
    [SerializeField] private float color_change_interval;
    [SerializeField] private Animator anim;
    [SerializeField] Transform _box_spawn;
    [SerializeField] Transform _box_despawn;
    private SpriteRenderer sprt_rndrr;
    private Rigidbody2D rgd_bdy;
    //private BoxCollider2D boxcollider;
    private float time_passed;
    private float _box_spawn_y;
    private float _box_despawn_y;


    // Start is called before the first frame update
    void Awake()
    {
        sprt_rndrr = GetComponent<SpriteRenderer>();
        rgd_bdy = GetComponent<Rigidbody2D>();
        //boxcollider = GetComponent<BoxCollider2D>();
        time_passed = 0;
        _box_spawn_y = _box_spawn.transform.localPosition.y;
        _box_despawn_y = _box_despawn.transform.localPosition.y;
    }
    public void BoxContructor(float pos_x, float size)
    {
        transform.localPosition =  new Vector2(pos_x, _box_spawn_y); //  -8<= x <= 8
        transform.localScale = new Vector2(size, size); // .5f < size < 2.0f
        time_passed = 0;

       // Debug.Log("spawn point: " + _box_spawn_y);
       // Debug.Log("spawn at: " + transform.localPosition);
        //rb.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }

    public void BoxDeconstructor()
    {
        rgd_bdy.velocity = Vector2.zero;
        anim.SetBool("clicked", false);
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("changeBoxColor", 3f, 2f);
        if(time_passed >= color_change_interval)
        {
            changeBoxColor();
            time_passed = 0;
        }
        time_passed += Time.deltaTime;

        if (transform.localPosition.y <= _box_despawn_y)
        {
            AssetManager.Instance.despawnBox(this);
        }
    }

    private void changeBoxColor()
    {
        sprt_rndrr.color = new Color(Random.value, Random.value, Random.value, 1.0f);
    }

    public Color getBoxColor()
    {
        return sprt_rndrr.color;
    }

    void OnMouseDown()
    {
        if (GameManager.Instance.getGameState())
        {
            //Debug.Log("clicked!");
            anim.SetBool("clicked", true);
            //AssetManager.Instance.despawnBox(this);
            GameManager.Instance.incrementScore();

        }
    }
}
