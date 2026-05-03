using UnityEngine;
using UnityEngine.InputSystem;

// Attach this to ANY object in the scene (e.g. GameManager)
// It will print to console so you can see in Android Logcat
// exactly what is and isn't working.

public class XRDebug : MonoBehaviour
{
    public InputActionReference leftPosition;   // XRI Left/Position
    public InputActionReference leftButton;     // XRI Left Interaction/Activate
    public Transform leftController;            // drag Left Controller here

    void OnEnable()
    {
        if (leftPosition != null) leftPosition.action.Enable();
        if (leftButton   != null)
        {
            leftButton.action.Enable();
            leftButton.action.performed += ctx => Debug.Log("[XRDebug] LEFT BUTTON PRESSED!");
        }
    }

    void Update()
    {
        // Log controller world position every 60 frames
        if (Time.frameCount % 60 == 0)
        {
            if (leftController != null)
                Debug.Log($"[XRDebug] Left Controller world pos: {leftController.position}");

            if (leftPosition != null)
                Debug.Log($"[XRDebug] Left Position action value: {leftPosition.action.ReadValue<Vector3>()}");
        }
    }
}