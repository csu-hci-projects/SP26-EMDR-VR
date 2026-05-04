using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
//five possible states 
    public enum GameState
    {
        Welcome,
        Instructions,
        Countdown,
        Playing,
        Paused,
        OutOfBounds
    }

    public GameState CurrentState = GameState.Welcome;
    public BallController ball;
    public TextMeshProUGUI instructionsText;

    void Awake() => Instance = this;

    void Start() => UpdateText();

    public void StartGame()
    {
        if (CurrentState == GameState.Welcome)
        {
            CurrentState = GameState.Instructions;
            UpdateText();
            return;
        }

        if (CurrentState == GameState.Instructions ||
            CurrentState == GameState.Paused ||
            CurrentState == GameState.OutOfBounds)
        {
            //triggers countdown before every launch
            StartCoroutine(CountdownThenLaunch());
            return;
        }
    }

    public void StopGame()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Paused;
        ball.Stop();
        UpdateText();
    }

    public void ResetGame()
    {
        CurrentState = GameState.OutOfBounds;
        ball.Stop();
        ball.transform.position = ball.StartPosition;
        UpdateText();
    }

    IEnumerator CountdownThenLaunch()
    {
        CurrentState = GameState.Countdown;

        instructionsText.text = "Get ready...\n\n3";
        yield return new WaitForSeconds(1f);

        instructionsText.text = "Get ready...\n\n2";
        yield return new WaitForSeconds(1f);

        instructionsText.text = "Get ready...\n\n1";
        yield return new WaitForSeconds(1f);

        instructionsText.text = "GO!";
        yield return new WaitForSeconds(0.5f);

        CurrentState = GameState.Playing;
        ball.Launch();
        UpdateText();
    }

    void UpdateText()
    {
        if (instructionsText == null) return;
//every state has its own instructions that are updated live as the state changes
        switch (CurrentState)
        {
            case GameState.Welcome:
                instructionsText.text =
                    "Hiii! Welcome to Ping Pong!\n" +
                    "When you are ready, go ahead\n" +
                    "and press A to proceed.";
                break;

            case GameState.Instructions:
                instructionsText.text =
                    "Use the RIGHT thumbstick to move the right paddle.\n" +
                    "Use the LEFT thumbstick to move the left paddle.\n\n" +
                    "When you feel comfortable, press A to begin!";
                break;

            case GameState.Countdown:
                // handled live in the coroutine
                break;

            case GameState.Playing:
                instructionsText.text = "Press A to pause";
                break;

            case GameState.Paused:
                instructionsText.text = "Paused\nPress A to proceed";
                break;

            case GameState.OutOfBounds:
                instructionsText.text =
                    "Oops! Looks like your ball went out of bounds!\n" +
                    "Press A to proceed.";
                break;
        }
    }
}
