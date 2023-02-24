using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Box temp_box;
    private SpriteRenderer sprt_rndrr;

    void Awake()
    {
        sprt_rndrr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
       // Debug.Log("collided ");
        if (col.gameObject.tag == "Box")
        {
            //Debug.Log("hit!");
            temp_box = col.gameObject.GetComponent<Box>();
            sprt_rndrr.color = temp_box.getBoxColor();
            if (GameManagerOld.Instance.getGameState())
            {
                GameManagerOld.Instance.decrementLife();
                AssetManager.Instance.despawnBox(temp_box);
            }
        }
    }
}
