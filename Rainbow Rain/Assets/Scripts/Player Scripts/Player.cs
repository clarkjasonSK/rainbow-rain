using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //tempporary avriables
    [Range (1,3)] [SerializeField] private int _player_color = 1;
    [SerializeField] private int _player_shell_health= 3;
    [SerializeField] private float _player_move_speed = 10f;
    // 1 = Cyan; 2 = Magenta; 3 = Yellow
    [SerializeField] private float _shell_starting_alpha = 1f;


    #region Player Variables
    private PlayerData _player_data;
    private PlayerController _player_controller;
    private PlayerControls _player_controls;
    private Vector2 _player_input;
    #endregion

    void Start()
    {
        this._player_data = new PlayerData(_player_shell_health);
        _player_controller = GetComponent<PlayerController>();

        _player_data.TotalShellHealth = _player_shell_health;
        _player_data.MoveSpeed = _player_move_speed;
        //playerController.MoveSpeed = _player_data._player_move_speed;

        _player_controls = InputManager.Instance.getControls();
        _player_controls.Enable();

        switch (_player_color)
        { 
            case 1:_player_data.PlayerColor = new Color(.5f, 1, 1, 0); 
                break;
            case 2:_player_data.PlayerColor = new Color(1, .5f, 1, 0);
                break;
            case 3:_player_data.PlayerColor = new Color(1, 1, .5f, 0);
                break;
        }


        _player_controller.setPlayerColor(_player_data.PlayerColor, 
                new Color(_player_data.PlayerColor.r - .3f * _player_data.PlayerColor.r, 
                            _player_data.PlayerColor.g - .3f * _player_data.PlayerColor.g, 
                            _player_data.PlayerColor.b - .3f * _player_data.PlayerColor.b,
                            _shell_starting_alpha));

    }

    void Update()
    {
        if (this._player_controls == null)
            return;
        if (!InputManager.Instance.InputAllowed)
            return;
        if (_player_data.CurrentShellHealth == -1)
        {
            return;
        }

        _player_input = this._player_controls.InGame.Movement.ReadValue<Vector2>();
        _player_controller.Move(_player_input, _player_data.MoveSpeed);
        //Debug.Log("input: " + _input);



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ProjectileBounds"))
        {
            return;
        }
        Projectile tempProj = collision.GetComponent<Projectile>();

        if(GameManager.Instance.compareColors(_player_data.PlayerColor, tempProj.getProjectileColor() ))
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
        _player_controller.setSoulColor(_player_data.PlayerColor);

    }

    private void damageToShell()
    {

        if (_player_data.CurrentShellHealth-- == 1) // decrement returns current value, so when it's 1, it will decrement to 0 but still pass the ocndition then destroyShell()
        {
            _player_controller.destroyShellCollider();
        }
        //_player_controller.decreaseShellColor(_shell_starting_alpha / _player_data.TotalShellHealth);
    }

    public Color getPlayerColor()
    {
        return _player_data.PlayerColor;
    }
}
