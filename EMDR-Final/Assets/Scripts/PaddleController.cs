using UnityEngine;
using UnityEngine.XR;

public class PaddleController : MonoBehaviour
{
    public bool isLeftController;
    public float paddleSpeed = 4f;
    public float yMin = -2.5f;
    public float yMax = 2.5f;

    private InputDevice device;
    private bool wasPressed = false;

    void Update()
    {
        // Always try to get device if not valid
        if (!device.isValid)
        {
            var node = isLeftController ? XRNode.LeftHand : XRNode.RightHand;
            //grabs the physical controller directly
            device = InputDevices.GetDeviceAtXRNode(node);
        }

        if (!device.isValid) return;
// reads raw thumbstick value 
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stick);
        if (Mathf.Abs(stick.y) > 0.1f)
        {
            Vector3 pos = transform.position;
            pos.y += stick.y * paddleSpeed * Time.deltaTime;
            //keeps paddle within bounds 
            pos.y = Mathf.Clamp(pos.y, yMin, yMax);
            transform.position = pos;
        }

        //Start/stop button 
        device.TryGetFeatureValue(CommonUsages.primaryButton, out bool pressed);
// only tiggers on button press, not per frame 
        if (pressed && !wasPressed)
            TriggerGameState();
        wasPressed = pressed;

#if UNITY_EDITOR
        // Keyboard fallback for editor testing
        if (UnityEngine.InputSystem.Keyboard.current != null)
        {
            if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
                TriggerGameState();
            if (isLeftController)
            {
                if (UnityEngine.InputSystem.Keyboard.current.upArrowKey.isPressed)
                {
                    Vector3 p = transform.position;
                    p.y = Mathf.Clamp(p.y + paddleSpeed * Time.deltaTime, yMin, yMax);
                    transform.position = p;
                }
                if (UnityEngine.InputSystem.Keyboard.current.downArrowKey.isPressed)
                {
                    Vector3 p = transform.position;
                    p.y = Mathf.Clamp(p.y - paddleSpeed * Time.deltaTime, yMin, yMax);
                    transform.position = p;
                }
            }
            else
            {
                if (UnityEngine.InputSystem.Keyboard.current.wKey.isPressed)
                {
                    Vector3 p = transform.position;
                    p.y = Mathf.Clamp(p.y + paddleSpeed * Time.deltaTime, yMin, yMax);
                    transform.position = p;
                }
                if (UnityEngine.InputSystem.Keyboard.current.sKey.isPressed)
                {
                    Vector3 p = transform.position;
                    p.y = Mathf.Clamp(p.y - paddleSpeed * Time.deltaTime, yMin, yMax);
                    transform.position = p;
                }
            }
        }
#endif
    }

    void TriggerGameState()
    {
        if (GameManager.Instance.CurrentState == GameManager.GameState.Countdown)
            return;

        if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
            GameManager.Instance.StopGame();
        else
            GameManager.Instance.StartGame();
    }
}
