using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    #region Player Variables

    private PlayerController _player_controller;
    private PlayerControls _player_controls;

    private PlayerData _player_data;

    private Vector2 _player_input;
    private Camera _camera;
    #endregion

    #region Game Values
    [SerializeField] [Range(1, 3)] private int PlayerColor = 1;
    [SerializeField] private int ShellHealth = 3;
    [SerializeField] private float ShellStartAlpha = 1f;

    [SerializeField] private float MoveSpeed = 10f;
    #endregion

    void Start()
    { 
        _player_controller = GetComponent<PlayerController>();
        _player_controls = InputManager.Instance.getControls();
        _player_controls.Enable();

        this._player_data = new PlayerData(ShellHealth, MoveSpeed);

        switch (PlayerColor)
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
                            ShellStartAlpha));

        _camera = GameObject.FindGameObjectWithTag(TagNames.MAIN_CAMERA).GetComponent<Camera>();
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

        _player_input = this._player_controls.InGame.Movement_KB.ReadValue<Vector2>();

        if (_player_input != Vector2.zero)
        {
            _player_controller.Traverse(_player_input, _player_data.MoveSpeed);
        }
        

        if (this._player_controls.InGame.Movement_M_Hold.ReadValue<float>()==1)
        {
            _player_input = _camera.ScreenToWorldPoint(this._player_controls.InGame.Movement_M_Position.ReadValue<Vector2>());

            _player_controller.Drag(_player_input, _player_data.MoveSpeed);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE_BOUNDS))
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
        //_player_data.MoveSpeed += 1f;
        _player_controller.setSoulColor(_player_data.PlayerColor);

    }

    private void damageToShell()
    {

        if (_player_data.CurrentShellHealth-- == 1) // decrement returns current value, so when it's 1, it will decrement to 0 AFTER checking the condition 
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
