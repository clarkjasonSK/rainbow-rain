using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    PROGRAM_START,
    MAIN_MENU,
    INGAME,
    PAUSED
}

public class GameManager : Singleton<GameManager>, ISingleton
{
    #region Singleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
    }
    #endregion

    #region Player References
    private Player _player_reference;
    public Player PlayerReference
    {
        get { return _player_reference; }
    }
    public Vector3 PlayerLocation
    {
        get { return _player_reference.transform.position; }
    }
    public Color PlayerColor
    {
        get { return _player_reference.PlayerColor; }
    }
    #endregion

    #region StateHandler Variables
    private StateHandler<GameState> _game_state_handler;
    public StateHandler<GameState> GameStateHandler
    {
        get { return _game_state_handler; }
    }
    public GameState GameState
    {
        get { return _game_state_handler.CurrentState; }
    }
    #endregion

    #region Event Variables
    Projectile projReference = null;
    #endregion

    public bool paused = false;
    void Update()
    {
        if(paused)
        {
            paused = false;
            EventBroadcaster.Instance.PostEvent(EventKeys.PAUSE_GAME, null);
        }
    }
    public void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        _player_reference = GameObject.FindWithTag(TagNames.PLAYER).GetComponent<Player>();
        AddObservers();

        isDone = true;
    }
    private void AddObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.START_MENU, OnStartMenu);
        EventBroadcaster.Instance.AddObserver(EventKeys.START_GAME, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.PLAYER_HIT, OnPlayerHit);
        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, OnGamePause);
    }

    public bool compareColors(Color playerColor, Color projColor)
    {
        if (playerColor.r == projColor.r &&
            playerColor.g == projColor.g &&
            playerColor.b == projColor.b)
        {
            return true;
        }
        return false;
    }

    #region Event Broadcaster Notifications
    public void OnStartMenu(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
        InputManager.Instance.toggleInputAllow(false);
    }
    public void OnGameStart(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.INGAME);
        InputManager.Instance.toggleInputAllow(true);

    }
    public void OnGamePause(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.PAUSED);
        InputManager.Instance.toggleInputAllow(false);

    }

    public void OnPlayerHit(EventParameters param)
    {
        //Player tempPlayer = param.GetParameter<Player>(EventParamKeys.playerParam, null);

        projReference = param.GetParameter<Projectile>(EventParamKeys.projParam, null);
        projReference.ProjectileActive = false;

        switch (GameState)
        {
            case GameState.MAIN_MENU:
                _player_reference.setPlayerColor(projReference.ProjectileColor);
                break;
            case GameState.INGAME:
                if (compareColors(_player_reference.PlayerColor, projReference.ProjectileColor))
                {
                    _player_reference.absorbToSoul();
                }
                else
                {
                    _player_reference.damageToShell();
                } 
                break;
        }

        ProjectileManager.Instance.removeProjectile(projReference);
    }
    #endregion
}

