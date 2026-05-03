using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    [Header("Assign from XRI Default Input Actions")]
    // Left:  XRI Left Interaction/Activate
    // Right: XRI Right Interaction/Activate
    public InputActionReference buttonAction;

    private void OnEnable()
    {
        if (buttonAction != null)
        {
            buttonAction.action.Enable();
            buttonAction.action.performed += OnButtonPressed;
        }
    }

    private void OnDisable()
    {
        if (buttonAction != null)
        {
            buttonAction.action.performed -= OnButtonPressed;
            buttonAction.action.Disable();
        }
    }

    void Update()
    {
        // Editor only — spacebar to test without headset
        #if UNITY_EDITOR
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            TriggerGameState();
        #endif
    }

    void OnButtonPressed(InputAction.CallbackContext ctx)
    {
        TriggerGameState();
    }

    void TriggerGameState()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Idle)
            GameManager.Instance.StartGame();
        else if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
            GameManager.Instance.StopGame();
        else if (GameManager.Instance.CurrentState == GameManager.GameState.Stopped)
            GameManager.Instance.ResetGame();
    }
}