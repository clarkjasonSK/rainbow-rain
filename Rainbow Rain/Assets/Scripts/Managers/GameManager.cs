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

[CreateAssetMenu(fileName = "GameManager", menuName = "ScriptableObjects/Singletons/GameManager")]
public class GameManager : SingletonSO<GameManager>, IInitializable, IEventObserver
{
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

    private List<LevelKey> _levels_list;
    public List<LevelKey> LevelsList
    {
        get { return _levels_list; }
    }

    public override void Initialize()
    {
        _game_state_handler = new StateHandler<GameState>();
        _game_state_handler.Initialize(GameState.PROGRAM_START);

        AddEventObservers();

        _levels_list = new List<LevelKey>();

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
    }
    public override void AddEventObservers()
    {
        EventBroadcaster.Instance.AddObserver(EventKeys.MENU_START, OnStartMenu);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_START, OnGameStart);
        EventBroadcaster.Instance.AddObserver(EventKeys.GAME_PAUSE, OnGamePause);
    }
    
    public void addLevel(LevelKey lvl)
    {
        _levels_list.Add(lvl);
    }

    #region Event Broadcaster Notifications
    public void OnStartMenu(EventParameters param=null)
    {
        _game_state_handler.Initialize(GameState.MAIN_MENU);
    }
    public void OnGameStart(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.INGAME);
        SceneManager.LoadScene(SceneNames.GAME_SCENE);

    }
    public void OnGamePause(EventParameters param = null)
    {
        _game_state_handler.Initialize(GameState.PAUSED);

    }
    
    #endregion
}

