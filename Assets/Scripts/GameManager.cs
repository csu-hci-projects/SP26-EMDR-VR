using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public enum GameState { Idle, Playing, Stopped }
    public GameState CurrentState = GameState.Idle;
    public BallController ball;
    public TextMeshProUGUI instructionsText;

    void Awake() => Instance = this;

    void Start()
    {
        UpdateText();
    }

    public void StartGame()
    {
        if (CurrentState != GameState.Idle) return;
        CurrentState = GameState.Playing;
        ball.Launch();
        UpdateText();
    }

    public void StopGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Stopped;
        ball.Stop();
        UpdateText();
    }

    public void ResetGame()
    {
        CurrentState = GameState.Idle;
        ball.ResetBall();
        UpdateText();
    }

    void UpdateText()
    {
        if (instructionsText == null) return;

        if (CurrentState == GameState.Idle)
            instructionsText.text = "Press A / X to Start\nLeft joystick → left paddle\nRight joystick → right paddle";
        else if (CurrentState == GameState.Playing)
            instructionsText.text = "Game in progress\nPress A / X to Stop";
        else if (CurrentState == GameState.Stopped)
            instructionsText.text = "Game paused\nPress A / X to Resume";
    }
}