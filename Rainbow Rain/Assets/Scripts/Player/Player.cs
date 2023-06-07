using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    #region Player Variables
    [SerializeField] private PlayerData _player_data;
    [SerializeField] private PlayerController _player_controller;

    [SerializeField] private InputHandler _input_handler;
    #endregion

    public Color PlayerColor
    {
        get { return _player_data.PlayerSoulColor; }
    }


    #region Game Values
    [SerializeField] GameSettings _game_settings;
    #endregion


    #region Event Parameters
    private EventParameters playerParam;
    #endregion

    void Start()
    {
        _game_settings = GameManager.Instance.GameSettings;

        this._player_data.Initialize(_game_settings.PlayerShellHealth);
        playerParam = new EventParameters();
        //playerHitEvent.AddParameter(EventParamKeys.playerParam, this);

        switch (_game_settings.PlayerColor)
        { 
            case 1:_player_data.PlayerSoulColor = ColorAtlas.getSpecifiedColor(ColorNames.CYAN);
                break;
            case 2:_player_data.PlayerSoulColor = ColorAtlas.getSpecifiedColor(ColorNames.MAGENTA);
                break;
            case 3:_player_data.PlayerSoulColor = ColorAtlas.getSpecifiedColor(ColorNames.YELLOW);
                break;
        }

        _player_data.CurrentPlayerColor = _player_data.PlayerSoulColor - ColorAtlas.WholeAlpha;

        _player_controller.SetSpriteColors(_player_data.CurrentPlayerColor, 
                new Color(_player_data.PlayerSoulColor.r - .3f * _player_data.PlayerSoulColor.r, 
                            _player_data.PlayerSoulColor.g - .3f * _player_data.PlayerSoulColor.g, 
                            _player_data.PlayerSoulColor.b - .3f * _player_data.PlayerSoulColor.b,
                            _game_settings.PlayerShellStartAlpha));

    }
    private void Update()
    {
        if (_input_handler.UserKeyHold)
        {
            movePlayer();
        }
        else if (_input_handler.UserCursorHold)
        {
            dragPlayer();
        }
    }
    private void movePlayer()
    {
        _player_controller.Traverse(_input_handler.UserKBInput, _game_settings.PlayerMoveSpeed);
    }
    private void dragPlayer()
    {
        // just a big block of text making sure it doesn't jitter at cursor point
        if (((_input_handler.UserCursorInput.x - _game_settings.PlayerCursorOffset) < transform.localPosition.x && transform.localPosition.x < (_input_handler.UserCursorInput.x + _game_settings.PlayerCursorOffset)) && (((_input_handler.UserCursorInput.y - _game_settings.PlayerCursorOffset) < transform.localPosition.y) && (transform.localPosition.y < (_input_handler.UserCursorInput.y + _game_settings.PlayerCursorOffset))))
            return;

        _player_controller.DragFollowTo(_input_handler.UserCursorInput, _game_settings.PlayerMoveSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE_BOUNDS))
        {
            return;
        }

        if (collision.CompareTag(TagNames.PROJECTILE))
        {
            playerParam.AddParameter(EventParamKeys.PROJ_PARAM, collision.GetComponent<Projectile>());

            EventBroadcaster.Instance.PostEvent(EventKeys.PLAYER_HIT, playerParam);
        }
        
    }

    public void AbsorbToSoul()
    {
        _player_data.increaseAlpha(_game_settings.PlayerAlphaIncrement);
        //_player_data.MoveSpeed += 1f;
        _player_controller.setSoulColor(_player_data.CurrentPlayerColor);

    }

    public void DamageToShell()
    {

        if (_player_data.CurrentShellHealth-- == 1) // decrement returns current value, so when it's 1, it will decrement to 0 AFTER checking the condition 
        {
            _player_controller.DestroyShellCollider();
        }
        //_player_controller.decreaseShellColor(_shell_starting_alpha / _player_data.TotalShellHealth);
    }


}
