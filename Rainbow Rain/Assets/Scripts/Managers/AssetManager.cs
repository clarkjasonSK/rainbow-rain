using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    public static AssetManager Instance;
    [SerializeField] BoxPool box_pool;
    [SerializeField] float box_spawn_rate; // box spawns every value

    private float time_passed;
    private Box temp_box;

    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //InvokeRepeating("spawnBox", 1f, 2f);

        if(time_passed >= box_spawn_rate)
        {
            spawnBox();
            time_passed = 0;
        }
        time_passed += Time.deltaTime;
    }

    public void setBoxSpawnRate(float spawn_rate)
    {
        box_spawn_rate = spawn_rate;
    }
    public void spawnBox()
    {
        //  (-8<= x <= 8, .5f < size < 2.0f )
        temp_box = box_pool.getBox();

        if(temp_box != null)
        {
            temp_box.BoxContructor(Random.Range(-8f, 8f), Random.Range(.5f, 2.0f));

        }

    }

    public void despawnBox(Box trash)
    {
        trash.BoxDeconstructor();
    }

}
