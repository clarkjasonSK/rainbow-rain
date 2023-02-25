using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerOld : MonoBehaviour
{
    public static GameManagerOld Instance;

    [SerializeField] private bool _game_state { get; set; }
    [SerializeField] private int _life_points;
    [SerializeField] private int _score;


    void Awake()
    {
        Instance = this;
        _game_state = true;
        _life_points = 100;
        _score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool getGameState()
    {
        return _game_state;
    }

    public void decrementLife()
    {
        _life_points--;
    }
    public void incrementScore()
    {
        _score++;
    }
}
