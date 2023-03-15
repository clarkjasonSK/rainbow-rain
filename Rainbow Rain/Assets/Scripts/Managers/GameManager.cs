using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    PROGRAM_START,
    MAIN_MENU,
    INGAME,
    PAUSED
}

public class GameManager : Singleton<GameManager>, ISingleton
{
    #region ISingleton Variables
    private bool isDone = false;
    public bool IsDoneInitializing
    {
        get { return isDone; }
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

    public void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        AddEventObservers();

        /*
        List<PatternData> patternList = JsonLoader.loadJsonData<PatternData>("Assets/JSON/Patterns.JSON", false);
        
        foreach(PatternData pat in patternList)
        {
            Debug.Log("====PATTERN id: " + pat.PatternID);
            Debug.Log("===pattern projs count: " + pat.PatternProjectiles.Count);
            Debug.Log("==proj repeatable: " + pat.PatternRepteatable);
            Debug.Log("==proj duration: " + pat.PatternDuration);
            foreach (int i in pat.PatternProjectiles)
            {
                Debug.Log("==proj id: " + i);
            }

        }*/
        
        isDone = true;
    }
    private void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.START_MENU, OnStartMenu);
        EventBroadcaster.Instance.AddObserver(EventKeys.START_GAME, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.PAUSE_GAME, OnGamePause);
    }
    

    #region Event Broadcaster Notifications
    public void OnStartMenu(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
    }
    public void OnGameStart(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.INGAME);
        SceneManager.LoadScene(SceneNames.GAME_SCENE);

    }
    public void OnGamePause(EventParameters param)
    {
        _game_state_handler.Initialize(GameState.PAUSED);

    }
    
    #endregion
}

