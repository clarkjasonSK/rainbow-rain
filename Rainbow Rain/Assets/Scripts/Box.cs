using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Box : MonoBehaviour
{
    [SerializeField] private float color_change_interval;
    [SerializeField] private Animator anim;
    private SpriteRenderer sprt_rndrr;
    private Rigidbody2D rgd_bdy;
    //private BoxCollider2D boxcollider;
    private float time_passed;
    

    // Start is called before the first frame update
    void Awake()
    {
        sprt_rndrr = GetComponent<SpriteRenderer>();
        rgd_bdy = GetComponent<Rigidbody2D>();
        //boxcollider = GetComponent<BoxCollider2D>();
        time_passed = 0;
    }
    public void BoxContructor(float pos_x, float size)
    {
        transform.localPosition =  new Vector2(pos_x, 6.0f); //  -8<= x <= 8
        transform.localScale = new Vector2(size, size); // .5f < size < 2.0f
        time_passed = 0;
        //rb.velocity = Vector2.zero;
        gameObject.SetActive(true);
    }

    public void BoxDeconstructor()
    {
        rgd_bdy.velocity = Vector2.zero;
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
            anim.SetBool("Clicked", true);
            //AssetManager.Instance.despawnBox(this);
            GameManager.Instance.incrementScore();

        }
    }
}
