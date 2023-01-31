using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gameBounds;
    void Start()
    {
        //float cameraResponsiveSize = 
        Debug.Log("BEFORE ortho size: " + Camera.main.orthographicSize + " gameBounds.x: " + gameBounds.bounds.size.x
            + " screen height: " + Screen.height + " screen width: " + Screen.width);
        Camera.main.orthographicSize = gameBounds.bounds.size.x * Screen.height / Screen.width * .5f; ;

        Debug.Log("AFTER ortho size: " + Camera.main.orthographicSize + " gameBounds.x: " + gameBounds.bounds.size.x
            + " screen height: " + Screen.height + " screen width: " + Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(" screen height: " + Screen.height + " screen width: " + Screen.width);
    }
}
