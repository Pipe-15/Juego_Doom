using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction shootAction;
    private InputAction weaponAKAction;
    private InputAction weaponShotgunAction;

    [HideInInspector]
    public Vector2 moveVector;

    void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move", false);
        shootAction = InputSystem.actions.FindAction("Attack", false);

        weaponAKAction =
            InputSystem.actions.FindAction("WeaponAK", false);

        weaponShotgunAction =
            InputSystem.actions.FindAction("WeaponShotgun", false);
    }

    void Update()
    {
        GetInput();
        Shoot();
        ChangeWeapon();
    }

    public void GetInput()
    {
        if (moveAction != null)
            moveVector = moveAction.ReadValue<Vector2>();
    }

    public void Shoot()
    {
        if (shootAction != null &&
            shootAction.WasPressedThisFrame())
        {
            if (GunController.Instance != null)
                GunController.Instance.Fire();
        }
    }

    void ChangeWeapon()
    {
        if (Keyboard.current == null ||
            GunController.Instance == null)
            return;

        // Tecla 1: AK-47.
        if (Keyboard.current.digit1Key.wasPressedThisFrame ||
            Keyboard.current.numpad1Key.wasPressedThisFrame)
        {
            GunController.Instance.EquiparAK();
        }

        // Tecla 2: escopeta.
        if (Keyboard.current.digit2Key.wasPressedThisFrame ||
            Keyboard.current.numpad2Key.wasPressedThisFrame)
        {
            GunController.Instance.EquiparEscopeta();
        }
    }
}