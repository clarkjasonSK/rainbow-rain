using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler: Singleton<InputHandler>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = true;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Player Variables
    private PlayerControls _player_controls = null;
    #endregion

    private Camera _camera;

    #region Input Variables
    private Vector2 _user_key_input;
    public Vector2 UserKeyInput
    {
        get {return _user_key_input;}
    }
    public bool UserKeyHold
    {
        get 
        {
            if (_user_key_input == Vector2.zero)
                return false;
            return true;
        }
    }

    private Vector2 _user_cursor_input;
    public Vector2 UserCursorInput
    {
        get { return _user_cursor_input; }
    }
    private float _user_cursorhold;
    public bool UserCursorHold
    {
        get
        {
            if (_user_cursorhold != 0)
                return true;
            return false;
        }
    }

    private bool _input_allowed;
    public bool InputAllowed
    {
        get { return this._input_allowed; }
    }
    #endregion

    private void Start()
    {
        if (_player_controls == null)
        {
            Initialize();
        }
    }
    public void Initialize()
    {

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

        _user_key_input = _player_controls.InGame.Movement_KB.ReadValue<Vector2>();
        
        _user_cursorhold = _player_controls.InGame.Movement_M_Hold.ReadValue<float>();

        
        if (_user_cursorhold == 1.0f)
        {

            _user_cursor_input = _camera.ScreenToWorldPoint(_player_controls.InGame.Movement_M_Position.ReadValue<Vector2>());
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
