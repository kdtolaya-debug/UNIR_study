using UnityEngine;

public class RayShot : MonoBehaviour
{
    [SerializeField] float rayLength = 10f;
    LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();

        // Si no tiene LineRenderer, usar solo el collider del sprite
        if (lr == null) return;

        lr.positionCount = 2;
    }

    private void Update()
    {
        if (lr == null) return;

        Vector2 origin = transform.position;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength);

        Vector3 endPoint = hit.collider != null
            ? (Vector3)hit.point
            : origin + direction * rayLength;

        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, endPoint);
    }

    // OnTriggerStay2D maneja el daño continuo via CapsuleCollider2D
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemySpaceShipFish>(out var enemy))
        {
            // collision.GetComponent<Enemy>()?.TakeDamage(1);
            Destroy(collision.gameObject); // destruye el enemy
            Debug.Log("Rayo golpeando: " + collision.name);
        }
    }
}