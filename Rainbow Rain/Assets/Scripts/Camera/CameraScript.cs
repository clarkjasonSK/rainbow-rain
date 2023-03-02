using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _game_area;

    void Start()
    {

        /*Debug.Log("BEFORE ortho size: " + Camera.main.orthographicSize + " gameBounds.x: " + _game_area.bounds.size.x
            + " screen height: " + Screen.height + " screen width: " + Screen.width);*/

        Camera.main.orthographicSize = _game_area.bounds.size.x * Screen.height / Screen.width * .5f;
        //InputManager.Instance.CameraReference = this.gameObject.GetComponent<Camera>();

        /*Debug.Log("AFTER ortho size: " + Camera.main.orthographicSize + " gameBounds.x: " + _game_area.bounds.size.x
            + " screen height: " + Screen.height + " screen width: " + Screen.width);*/
    }


}
