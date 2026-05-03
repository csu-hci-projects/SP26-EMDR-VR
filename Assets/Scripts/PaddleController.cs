using UnityEngine;
using UnityEngine.InputSystem;

public class PaddleController : MonoBehaviour
{
    public bool isLeftController;
    public float paddleSpeed = 4f;
    public float yMin = -2.5f;
    public float yMax = 2.5f;

    [Header("Assign from XRI Default Input Actions")]
    public InputActionReference joystickAction;

    public InputActionReference buttonAction;

    private void OnEnable()
    {
        if (joystickAction != null) joystickAction.action.Enable();
        if (buttonAction   != null) buttonAction.action.Enable();

        if (buttonAction != null)
            buttonAction.action.performed += OnButtonPressed;
    }

    private void OnDisable()
    {
        if (buttonAction != null)
            buttonAction.action.performed -= OnButtonPressed;

        if (joystickAction != null) joystickAction.action.Disable();
        if (buttonAction   != null) buttonAction.action.Disable();
    }

    void Update()
    {
        HandlePaddleMovement();

        #if UNITY_EDITOR
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            TriggerGameState();
        #endif
    }

    void HandlePaddleMovement()
    {
        if (GameManager.Instance.CurrentState != GameManager.GameState.Playing) return;

        float moveY = 0f;

        #if UNITY_EDITOR
        if (isLeftController)
        {
            if (Keyboard.current.upArrowKey.isPressed)   moveY =  1f;
            if (Keyboard.current.downArrowKey.isPressed) moveY = -1f;
        }
        else
        {
            if (Keyboard.current.wKey.isPressed) moveY =  1f;
            if (Keyboard.current.sKey.isPressed) moveY = -1f;
        }
        #else
        if (joystickAction != null)
        {
            Vector2 stick = joystickAction.action.ReadValue<Vector2>();
            moveY = stick.y;
        }
        #endif

        Vector3 pos = transform.position;
        pos.y += moveY * paddleSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
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