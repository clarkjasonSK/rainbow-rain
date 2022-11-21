using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPool : MonoBehaviour
{
    [SerializeField] private Transform boxes_empty;
    [SerializeField] private Box box_template;
    [SerializeField] private float box_max;

    private List<Box> box_list;
    private Box temp_box;

    void Awake()
    {
        box_list = new List<Box>();
        for (int i = 0; i < box_max; i++)
        {
            temp_box = Instantiate(box_template);
            temp_box.transform.parent = boxes_empty.transform;
            temp_box.gameObject.SetActive(true);
            box_list.Add(temp_box);
            temp_box.gameObject.SetActive(false);
        }
    }

    public Box getBox()
    {
        for (int i = 0; i < box_max; i++)
        {
            if (!box_list[i].gameObject.activeInHierarchy)
            {
                return box_list[i];
            }
        }
        return null;
    }
}
