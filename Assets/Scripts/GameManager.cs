
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Idle, Playing, Stopped}
    public GameState CurrentState = GameState.Idle;

    public BallController ball;

    void Awake() => Instance = this;

    public void StartGame()
    {
        if (CurrentState != GameState.Idle) return;
        CurrentState = GameState.Playing;
        ball.Launch();
        Debug.Log("Game Started");
    }

    public void StopGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Stopped;
        ball.Stop();
        Debug.Log("Game Stopped");
    }

    public void ResetGame()
    {
        CurrentState = GameState.Idle;
        ball.ResetBall();
        Debug.Log("Game Reset");
    }

}