using UnityEngine;
using UnityEngine.XR;
using System.Collections.Generic;

public class PaddleController : MonoBehaviour
{
    public bool isLeftController;

    private InputDevice controller;
    private bool wasButtonPressed = false;

    void Start()
    {
        var characteristics = isLeftController
            ? InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller
            : InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;

        var devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
        if (devices.Count > 0) controller = devices[0];
    }

    void Update()
    {
        bool buttonPressed = false;
        controller.TryGetFeatureValue(CommonUsages.primaryButton, out buttonPressed);

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