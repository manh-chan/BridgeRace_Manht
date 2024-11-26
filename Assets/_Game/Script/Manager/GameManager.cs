using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public GameState state;
    public static event Action<GameState> OnStateChanged;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        UpdateGameState(GameState.StartGame);
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.StartGame:
                HandleStartGame();
                break;
            case GameState.PlayGame:
                HandlePlayGame();
                break;
            case GameState.Victory:
                HandleVictory();
                break;
            case GameState.fail:
                HandleFail();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnStateChanged?.Invoke(newState);
    }
    private void HandleStartGame()
    {
        UIManager.Instance.Open<CanvasMainMenu>();
    }
    private void HandlePlayGame()
    {
        UIManager.Instance.Open<CanvasGamePlay>();
    }
    private void HandleVictory()
    {
        UIManager.Instance.Open<CanvasVictory>();
    }
    private void HandleFail()
    {
        UIManager.Instance.Open<CanvasFail>();
    }
    public enum GameState
    {
        StartGame,
        PlayGame,
        Victory,
        fail,
    }
}
