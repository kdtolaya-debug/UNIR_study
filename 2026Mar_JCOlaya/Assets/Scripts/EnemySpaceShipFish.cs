using UnityEngine;

public class EnemySpaceShipFish : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float leftLimitCoordinate = -3f;
    [SerializeField] float rightScreenLimit = 3f;
    bool isGoingLeft = true;

    void Start()
    {
        Destroy(gameObject, 5f);
    }
    void Update()
    {
        Vector2 direction = isGoingLeft ? Vector2.left : Vector2.right;
        transform.Translate(direction * speed * Time.deltaTime);
        if (isGoingLeft && (transform.position.x < leftLimitCoordinate))
        {            isGoingLeft = false;        }
        if (!isGoingLeft && (transform.position.x > rightScreenLimit))
        {            Destroy(gameObject);        }
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerShot"))
        {
            Destroy(gameObject);
            //Destroy(collision.collider.gameObject);
        }
    }
}
