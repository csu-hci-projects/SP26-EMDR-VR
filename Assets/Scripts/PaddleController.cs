using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using XRInputDevice = UnityEngine.XR.InputDevice;
using XRCommonUsages = UnityEngine.XR.CommonUsages;

public class PaddleController : MonoBehaviour
{
    public bool isLeftController;
    public float paddleSpeed = 4f;
    public float yMin = -2.5f;
    public float yMax = 2.5f;

    private XRInputDevice controller;
    private bool controllerFound = false;  
    private bool wasButtonPressed = false;

    void Start()
    {
    }

    void Update()
    {
        if (!controllerFound)
            TryFindController();

        HandleStartStop();
        HandlePaddleMovement();
    }

    void TryFindController()
    {
        var characteristics = isLeftController
            ? InputDeviceCharacteristics.Left  | InputDeviceCharacteristics.Controller
            : InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        var devices = new List<XRInputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);

        if (devices.Count > 0)
        {
            controller = devices[0];
            controllerFound = true;
            Debug.Log($"[PaddleController] {(isLeftController ? "Left" : "Right")} controller found: {controller.name}");
        }
    }

    void HandleStartStop()
    {
        bool buttonPressed = false;

        #if UNITY_EDITOR
            buttonPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        #else
            // Only read input if we actually have a controller
            if (controllerFound)
                controller.TryGetFeatureValue(XRCommonUsages.primaryButton, out buttonPressed);
        #endif

        if (buttonPressed && !wasButtonPressed)
        {
            if (GameManager.Instance.CurrentState == GameManager.GameState.Idle)
                GameManager.Instance.StartGame();
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Playing)
                GameManager.Instance.StopGame();
            else if (GameManager.Instance.CurrentState == GameManager.GameState.Stopped)
                GameManager.Instance.ResetGame();
        }

        wasButtonPressed = buttonPressed;
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
            if (controllerFound)
            {
                Vector2 joystick;
                controller.TryGetFeatureValue(XRCommonUsages.primary2DAxis, out joystick);
                moveY = joystick.y;
            }
        #endif

        Vector3 pos = transform.position;
        pos.y += moveY * paddleSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
    }
}