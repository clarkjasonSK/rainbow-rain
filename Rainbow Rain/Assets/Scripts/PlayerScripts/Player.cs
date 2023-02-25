using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class Player : MonoBehaviour
{
    #region Player Variables
    private PlayerController _player_controller;
    private PlayerData _player_data;
    #endregion

    #region Event Parameters
    private EventParameters playerHitEvent;
    #endregion

    public Color PlayerColor
    {
        get { return _player_data.PlayerSoulColor; }
    }

    #region Game Values
    [SerializeField] [Range(1, 3)] private int PlayerIntColor = 1;
    [SerializeField] private int ShellHealth = 3;
    [SerializeField] private float ShellStartAlpha = 1f;

    [SerializeField] private float MoveSpeed = 10f;
    #endregion

    void Start()
    { 
        _player_controller = GetComponent<PlayerController>();
        this._player_data = new PlayerData(ShellHealth, MoveSpeed);
        playerHitEvent = new EventParameters();
        //playerHitEvent.AddParameter(EventParamKeys.playerParam, this);

        switch (PlayerIntColor)
        { 
            case 1:_player_data.PlayerSoulColor = ColorDictionary.getSpecifiedColor(ColorNames.CYAN);
                break;
            case 2:_player_data.PlayerSoulColor = ColorDictionary.getSpecifiedColor(ColorNames.MAGENTA);
                break;
            case 3:_player_data.PlayerSoulColor = ColorDictionary.getSpecifiedColor(ColorNames.YELLOW);
                break;
        }
        _player_data.CurrentPlayerColor = _player_data.PlayerSoulColor - new Color(0,0,0,1);

        _player_controller.setPlayerColor(_player_data.CurrentPlayerColor, 
                new Color(_player_data.PlayerSoulColor.r - .3f * _player_data.PlayerSoulColor.r, 
                            _player_data.PlayerSoulColor.g - .3f * _player_data.PlayerSoulColor.g, 
                            _player_data.PlayerSoulColor.b - .3f * _player_data.PlayerSoulColor.b,
                            ShellStartAlpha));

    }

    public void movePlayer(Vector2 playerInput)
    {
        _player_controller.Traverse(playerInput, _player_data.MoveSpeed);
    }
    public void dragPlayer(Vector2 playerInput)
    {
        _player_controller.Drag(playerInput, _player_data.MoveSpeed);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagNames.PROJECTILE_BOUNDS))
        {
            return;
        }

        if (collision.CompareTag(TagNames.PROJECTILE))
        {
            //NotifyPlayerHit(this, collision.GetComponent<Projectile>());
            playerHitEvent.AddParameter(EventParamKeys.projParam, collision.GetComponent<Projectile>());

            EventBroadcaster.Instance.PostEvent(EventKeys.PLAYER_HIT, playerHitEvent);
        }
        
    }

    public void absorbToSoul()
    {
        _player_data.increaseAlpha(.10f);
        //_player_data.MoveSpeed += 1f;
        _player_controller.setSoulColor(_player_data.CurrentPlayerColor);

    }

    public void damageToShell()
    {

        if (_player_data.CurrentShellHealth-- == 1) // decrement returns current value, so when it's 1, it will decrement to 0 AFTER checking the condition 
        {
            _player_controller.destroyShellCollider();
        }
        //_player_controller.decreaseShellColor(_shell_starting_alpha / _player_data.TotalShellHealth);
    }

    public void setPlayerColor(Color color)
    {
        //_player_data.PlayerColor = color;
        _player_controller.setPlayerColor(color, color);
    }

}
