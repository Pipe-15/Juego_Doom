
using UnityEngine;
using UnityEngine.InputSystem;

// Lee las entradas del jugador mediante el nuevo Input System.
public class InputController : MonoBehaviour
{
    // Acción configurada para mover al jugador.
    private InputAction moveAction;

    // Acción configurada para disparar.
    private InputAction shootAction;

    // Acción configurada para saltar.
    private InputAction jumpAction;

    // Vector de movimiento que utiliza PlayerMovement.
    [HideInInspector]
    public Vector2 moveVector;

    // Indica si se acaba de pulsar el botón de salto.
    [HideInInspector]
    public bool IsJumping;


    // Se ejecuta al inicializar el objeto.
    private void Awake()
    {
        // Busca las acciones por sus nombres
        // dentro de la configuración del Input System.
        moveAction = InputSystem.actions.FindAction("Move");
        shootAction = InputSystem.actions.FindAction("Attack");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }


    // Lee las entradas en cada actualización del juego.
    private void Update()
    {
        // Lee el movimiento.
        GetInput();

        // Comprueba si se ha disparado.
        Shoot();

        // Comprueba si se ha pulsado el salto.
        GetJumpInput();
    }


    // Lee la dirección de movimiento.
    public void GetInput()
    {
        // Comprueba que la acción exista antes de leerla.
        if (moveAction != null)
        {
            // Obtiene un vector con los valores de movimiento.
            moveVector = moveAction.ReadValue<Vector2>();
        }
    }


    // Gestiona la acción de disparar.
    public void Shoot()
    {
        // Comprueba si existe la acción y si se acaba
        // de pulsar el botón de disparo.
        if (shootAction != null &&
            shootAction.WasPressedThisFrame())
        {
            // Comprueba que exista el controlador del arma.
            if (GunController.Instance != null)
            {
                // Solicita al arma que dispare.
                GunController.Instance.Fire();
            }
        }
    }


    // Detecta si el jugador acaba de pulsar el salto.
    private void GetJumpInput()
    {
        // Es true únicamente durante el frame en que se pulsa.
        // Si no existe la acción, permanece en false.
        IsJumping = jumpAction != null &&
                    jumpAction.WasPressedThisFrame();
    }
}
