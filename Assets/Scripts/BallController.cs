using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    public Transform paddleLeft;
    public Transform paddleRight;
    public AudioClip pingSound;

    private Vector3 direction;
    private Vector3 startPosition;
    private bool isMoving = false;
    private AudioSource audioSource;

    void Start()
    {
        startPosition = transform.position;
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
    }

    void Update()
    {
        if (!isMoving) return;

        transform.position += direction * speed * Time.deltaTime;

        if (transform.position.y >= 3f || transform.position.y <= -3f)
        {
            direction.y = -direction.y;
        }

        if (transform.position.x <= paddleLeft.position.x + 0.5f && direction.x < 0)
        {
            if (Mathf.Abs(transform.position.y - paddleLeft.position.y) < 1.2f)
            {
                // Random angle bounce
                float randomY = Random.Range(-0.8f, 0.8f);
                direction = new Vector3(1f, randomY, 0f).normalized;
                PlayPing(paddleLeft.position);
                HapticFeedback(true);
            }
            else
            {
                // Missed the paddle - reset ball
                ResetBall();
            }
        }

        if (transform.position.x >= paddleRight.position.x - 0.5f && direction.x > 0)
        {
            if (Mathf.Abs(transform.position.y - paddleRight.position.y) < 1.2f)
            {
                float randomY = Random.Range(-0.8f, 0.8f);
                direction = new Vector3(-1f, randomY, 0f).normalized;
                PlayPing(paddleRight.position);
                HapticFeedback(false);
            }
            else
            {
                ResetBall();
            }
        }
    }

    void PlayPing(Vector3 position)
    {
        if (pingSound != null)
        {
            audioSource.transform.position = position;
            audioSource.PlayOneShot(pingSound);
        }
    }

    void HapticFeedback(bool isLeft)
    {
        #if !UNITY_EDITOR
        var characteristics = isLeft
            ? UnityEngine.XR.InputDeviceCharacteristics.Left
            : UnityEngine.XR.InputDeviceCharacteristics.Right;

        var devices = new System.Collections.Generic.List<UnityEngine.XR.InputDevice>();
        UnityEngine.XR.InputDevices.GetDevicesWithCharacteristics(
            characteristics | UnityEngine.XR.InputDeviceCharacteristics.Controller, devices);

        if (devices.Count > 0)
        {
            UnityEngine.XR.HapticCapabilities caps;
            if (devices[0].TryGetHapticCapabilities(out caps) && caps.supportsImpulse)
                devices[0].SendHapticImpulse(0, 0.7f, 0.1f);
        }
        #endif
    }

    public void Launch()
    {
        float randomY = Random.Range(-0.5f, 0.5f);
        direction = new Vector3(1f, randomY, 0f).normalized;
        isMoving = true;
    }

    public void Stop()
    {
        isMoving = false;
    }

    public void ResetBall()
    {
        Stop();
        transform.position = startPosition;
        direction = Vector3.zero;
        Invoke("Launch", 0.5f);
    }
}