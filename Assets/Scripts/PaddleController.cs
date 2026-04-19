using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using XRInputDevice = UnityEngine.XR.InputDevice;

public class PaddleController : MonoBehaviour
{
    public bool isLeftController;

    private XRInputDevice controller;
    private bool wasButtonPressed = false;

    void Start()
    {
        var characteristics = isLeftController
            ? InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller
            : InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        var devices = new List<XRInputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        if (devices.Count > 0) controller = devices[0];
    }

    void Update()
    {
        bool buttonPressed = false;

        #if UNITY_EDITOR
            buttonPressed = Keyboard.current.spaceKey.wasPressedThisFrame;
        #else
            controller.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed);
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
}