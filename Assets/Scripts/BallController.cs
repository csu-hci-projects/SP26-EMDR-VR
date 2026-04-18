using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5f;
    private Vector3 direction;
    private Vector3 startPosition;
    private bool isMoving = false;

    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (!isMoving) return;
        transform.position += direction * speed * Time.deltaTime;
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
    }
    public void Bounce()
    {
        direction = -direction;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            Bounce();
        }
    }
}