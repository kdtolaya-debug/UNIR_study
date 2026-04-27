using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceshipController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float linearVelocity = 3f;
    [SerializeField] InputActionReference move;

    [Header("Sistema de Disparo")]
    [SerializeField] InputActionReference shoot;
    [SerializeField] GameObject prefabShot;
    [SerializeField] Transform shottingPoint;
    [SerializeField] InputActionReference shootHold;

    [Header("Estado Actual")]
    [SerializeField] GameObject currentPrefabShot;
    [SerializeField] int specialAmmo = 0;

    private GameObject activeRay = null;
    private bool isRayPowerUp = false;

    Rigidbody2D rb2d;
    Vector2 rawMove = Vector2.zero;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentPrefabShot = prefabShot;
        specialAmmo = 0;
    }

    private void OnEnable()
    {
        move.action.Enable();
        shoot.action.Enable();

        move.action.started += OnMove;
        move.action.performed += OnMove;
        move.action.canceled += OnMove;

        shoot.action.started += OnShootStarted;

        if (shootHold != null)
        {
            shootHold.action.Enable();
            shootHold.action.started += OnRayStarted;
            shootHold.action.canceled += OnRayCanceled;
        }
    }

    private void OnDisable()
    {
        move.action.Disable();
        shoot.action.Disable();

        move.action.started -= OnMove;
        move.action.performed -= OnMove;
        move.action.canceled -= OnMove;

        shoot.action.started -= OnShootStarted;

        if (shootHold != null)
        {
            shootHold.action.Disable();
            shootHold.action.started -= OnRayStarted;
            shootHold.action.canceled -= OnRayCanceled;
        }
    }

    private void Update()
    {
        rb2d.linearVelocity = rawMove * linearVelocity;
    }

    void OnMove(InputAction.CallbackContext context)
    {
        rawMove = context.action.ReadValue<Vector2>();
    }

    private void OnShootStarted(InputAction.CallbackContext context)
    {
        if (isRayPowerUp) return;
        if (currentPrefabShot == null) return;

        Instantiate(currentPrefabShot, shottingPoint.position, Quaternion.identity);
        ConsumeSpecialAmmo();
    }

    private void OnRayStarted(InputAction.CallbackContext context)
    {
        if (!isRayPowerUp) return;
        if (activeRay != null) return;
        if (currentPrefabShot == null) return;

        activeRay = Instantiate(currentPrefabShot,
            shottingPoint.position,
            shottingPoint.rotation); // respeta la rotación del punto de disparo
        activeRay.transform.SetParent(shottingPoint);
        ConsumeSpecialAmmo();
    }

    private void OnRayCanceled(InputAction.CallbackContext context)
    {
        if (activeRay != null)
        {
            Destroy(activeRay);
            activeRay = null;
        }
    }

    private void ConsumeSpecialAmmo()
    {
        if (specialAmmo <= 0) return;

        specialAmmo--;
        Debug.Log($"Munición especial restante: {specialAmmo}");

        if (specialAmmo <= 0)
            ResetToDefaultShot();
    }

    private void ResetToDefaultShot()
    {
        if (activeRay != null)
        {
            Destroy(activeRay);
            activeRay = null;
        }

        currentPrefabShot = prefabShot;
        isRayPowerUp = false;
        specialAmmo = 0;
        Debug.Log("Munición especial agotada. Regresando al disparo base.");
    }

    public void EquipPowerUp(GameObject newPrefab, int uses, bool isRay = false)
    {
        if (activeRay != null)
        {
            Destroy(activeRay);
            activeRay = null;
        }

        currentPrefabShot = newPrefab;
        specialAmmo = uses;
        isRayPowerUp = isRay;

        Debug.Log($"Power-up equipado: {newPrefab.name} | Usos: {uses} | Rayo: {isRay}");
    }
}