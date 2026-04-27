using UnityEngine;

public class DoubleShot : MonoBehaviour
{
    private float speed = 8.0f;
    Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rb2d.linearVelocity = Vector2.right * speed;
        Destroy(gameObject, 3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}