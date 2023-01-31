using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerControls _player_controls = null;
    private bool _input_allowed;
    public bool InputAllowed
    {
        get { return this._input_allowed; }
    }
    public void Awake()
    {
        initialize();
    }
    public void initialize()
    {
        {
            if (_player_controls == null)
            {
                _player_controls = new PlayerControls();
            }
            this._input_allowed = true;
        }
    }

    public PlayerControls getControls()
    {
        if (this._player_controls == null)
        {
            initialize();
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
