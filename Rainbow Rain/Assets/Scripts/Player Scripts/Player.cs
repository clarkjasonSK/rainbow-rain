using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //tempporary avriables
    [Range (1,3)] [SerializeField] private int playerColor = 1;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private float playerMoveSpeed = 10f;
    // 1 = Cyan; 2 = Magenta; 3 = Yellow
    [SerializeField] private float shellStartingAlpha = 1f;


    #region Player Variables
    private PlayerData _player_data;
    [SerializeField] private PlayerController playerController;
    private PlayerControls playerControls;
    private Vector2 playerInput;
    #endregion

    void Start()
    {
        this._player_data = new PlayerData(playerLives);

        _player_data.TotalLives = playerLives;
        _player_data.MoveSpeed = playerMoveSpeed;
        //playerController.MoveSpeed = _player_data.MoveSpeed;

        playerControls = InputManager.Instance.getControls();
        playerControls.Enable();

        switch (playerColor)
        { 
            case 1:_player_data.PlayerColor = new Color(.5f, 1, 1, 0); 
                break;
            case 2:_player_data.PlayerColor = new Color(1, .5f, 1, 0);
                break;
            case 3:_player_data.PlayerColor = new Color(1, 1, .5f, 0);
                break;
        }
        playerController.initControllerComponents(GetComponent<Rigidbody2D>(), GetComponent<CircleCollider2D>());
        playerController.initPlayerColor(_player_data.PlayerColor, 
                new Color(_player_data.PlayerColor.r - .3f * _player_data.PlayerColor.r, 
                            _player_data.PlayerColor.g - .3f * _player_data.PlayerColor.g, 
                            _player_data.PlayerColor.b - .3f * _player_data.PlayerColor.b,
                            shellStartingAlpha));

    }

    void Update()
    {
        if (this.playerControls == null)
            return;
        if (!InputManager.Instance.InputAllowed)
            return;
        if (_player_data.CurrentLives == -1)
            return;

        playerInput = this.playerControls.InGame.Movement.ReadValue<Vector2>();
        playerController.Move(playerInput, _player_data.MoveSpeed);
        //Debug.Log("input: " + _input);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile tempProj = collision.GetComponent<Projectile>();
        if(GameManager.Instance.compareColors(_player_data.PlayerColor, tempProj.ProjData.ProjectileColor))
        {
            absorbToSoul();
        }
        else
        {
            damageToShell();
        }
        
    }

    private void absorbToSoul()
    {
        _player_data.increaseAlpha(.10f);
        _player_data.MoveSpeed += 1f;
        playerController.setSoulColor(_player_data.PlayerColor);

    }

    private void damageToShell()
    {

        if (_player_data.CurrentLives-- == 1)
        {
            playerController.destroyShell();
        }
        playerController.decreaseShellColor(shellStartingAlpha / _player_data.TotalLives);
    }
}
