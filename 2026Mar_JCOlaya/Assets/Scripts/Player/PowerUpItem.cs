using UnityEngine;

public class PowerUpItem : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] GameObject powerUpPrefab;
    [SerializeField] int ammoAmount = 3;

    [Tooltip("Marca esto si el poder es un RAYO SOSTENIDO. " +
             "Deja sin marcar si es un DISPARO DOBLE (instantáneo).")]
    [SerializeField] bool isRayPowerUp = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        SpaceshipController player = collision.GetComponent<SpaceshipController>();

        if (player != null)
        {
            player.EquipPowerUp(powerUpPrefab, ammoAmount, isRayPowerUp);
            Destroy(gameObject);
        }
    }
}