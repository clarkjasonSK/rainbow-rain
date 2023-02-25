using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>, ISingleton
{
    #region Player Variables
    private PlayerControls _player_controls = null;
    private Player _player_reference = null;
    #endregion
    private Camera _camera;
    private Vector2 _player_input;

    private bool _input_allowed;
    public bool InputAllowed
    {
        get { return this._input_allowed; }
    }

    private bool isDone = true;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    public void Initialize()
    {
        _player_reference = GameManager.Instance.PlayerReference;
        if (_player_controls == null)
        {
            _player_controls = new PlayerControls();
            _player_controls.Enable();
        }
        this._input_allowed = true;
        

        _camera = GameObject.FindGameObjectWithTag(TagNames.MAIN_CAMERA).GetComponent<Camera>();

        isDone = true;
    }

    void Update()
    {
        if (!_input_allowed)
            return;

        _player_input = _player_controls.InGame.Movement_KB.ReadValue<Vector2>();

        if (_player_input != Vector2.zero)
        {
            _player_reference.movePlayer(_player_input);
        }

        if (_player_controls.InGame.Movement_M_Hold.ReadValue<float>() == 1)
        {
            _player_reference.dragPlayer(_camera.ScreenToWorldPoint(_player_controls.InGame.Movement_M_Position.ReadValue<Vector2>()));
        }
    }

    public PlayerControls getControls()
    {
        if (this._player_controls == null)
        {
            Initialize();
        }

        return this._player_controls;
    }
    public void toggleDevice(InputDevice device, bool active)
    {
        if (active)
            InputSystem.EnableDevice(device);
        else
            InputSystem.DisableDevice(device);
    }

    public void toggleInputAllow(bool state)
    {
        this._input_allowed = state;
    }
}
