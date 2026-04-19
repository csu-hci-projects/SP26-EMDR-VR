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

        if (transform.position.x <= paddleLeft.position.x + 0.5f && direction.x < 0)
        {
            direction.x = Mathf.Abs(direction.x);
            PlayPing(paddleLeft.position); 
        }

        if (transform.position.x >= paddleRight.position.x - 0.5f && direction.x > 0)
        {
            direction.x = -Mathf.Abs(direction.x);
            PlayPing(paddleRight.position); 
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

    public void Launch()
    {
        direction = Vector3.right;
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
    }
}