using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    private float speed = 5.0f;
    Rigidbody2D rb2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
