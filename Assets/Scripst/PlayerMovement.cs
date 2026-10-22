
using UnityEngine;

// Exige que el jugador tenga un Character Controller.
// Este componente controla las colisiones y el movimiento del jugador.
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]

    // Velocidad con la que el jugador camina.
    public float speed = 6f;

    [Header("Salto y gravedad")]

    // Altura aproximada que alcanza el jugador al saltar.
    public float jumpHeight = 2f;

    // Fuerza de la gravedad aplicada al jugador.
    public float gravity = -20f;

    // Guarda la velocidad vertical del jugador.
    // Un valor positivo hace que suba y uno negativo que caiga.
    private float verticalVelocity;

    // Referencia al componente que lee los controles.
    private InputController inputController;

    // Referencia al componente que mueve y colisiona al jugador.
    private CharacterController characterController;

    // Plataforma móvil sobre la que está apoyado el jugador.
    private PlataformaMovil plataformaActual;

    // Se ejecuta cuando se inicializa este componente.
    private void Awake()
    {
        // Busca el controlador de entrada en el mismo objeto.
        inputController = GetComponent<InputController>();

        // Busca el controlador de colisiones del jugador.
        characterController = GetComponent<CharacterController>();
    }

    // Se ejecuta una vez por cada fotograma del juego.
    private void Update()
    {
        // Evita intentar mover al jugador si falta algún componente.
        if (inputController == null || characterController == null)
        {
            return;
        }

        // Si la plataforma fue destruida o dejó de estar disponible,
        // limpiamos la referencia sin intentar cambiar la jerarquía
        // durante la desactivación de un objeto.
        if (plataformaActual == null)
        {
            plataformaActual = null;
        }

        // Comprueba si el jugador está intentando saltar.
        if (characterController.isGrounded && inputController.IsJumping)
        {
            // Separa al jugador antes de iniciar el salto.
            SepararDePlataforma();

            // Calcula la velocidad inicial necesaria para saltar.
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Si el jugador está en el suelo y cae,
        // mantiene una pequeña velocidad hacia abajo.
        if (characterController.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }

        // Aplica la gravedad usando el tiempo entre fotogramas.
        verticalVelocity += gravity * Time.deltaTime;

        // Lee el movimiento horizontal y vertical del teclado.
        Vector3 inputVector = new Vector3(
            inputController.moveVector.x,
            0f,
            inputController.moveVector.y
        );

        // Convierte el movimiento local según la orientación del jugador.
        inputVector = transform.TransformDirection(inputVector);

        // Ajusta la velocidad de caminar.
        inputVector *= speed;

        // Añade la velocidad de salto o caída.
        inputVector.y = verticalVelocity;

        // Mueve al jugador mediante su Character Controller.
        characterController.Move(inputVector * Time.deltaTime);
    }

    // Unity llama a este método cuando el Character Controller
    // entra en contacto con otro collider durante su movimiento.
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Solo consideramos suelo una superficie cuya normal
        // apunta suficientemente hacia arriba.
        if (hit.normal.y < 0.5f)
        {
            // Ignora paredes y contactos laterales.
            return;
        }

        // Comprueba si el objeto golpeado pertenece a una plataforma móvil.
        PlataformaMovil plataforma =
            hit.collider.GetComponentInParent<PlataformaMovil>();

        // Si encontramos una plataforma móvil, seguimos su movimiento
        // convirtiendo al jugador en hijo de ella.
        if (plataforma != null)
        {
            // Evita modificar la jerarquía innecesariamente.
            if (plataformaActual != plataforma)
            {
                // Guarda la referencia de la plataforma actual.
                plataformaActual = plataforma;

                // Conserva la posición mundial del jugador.
                transform.SetParent(plataforma.transform, true);
            }
        }
        else
        {
            // Si el jugador toca otro suelo, deja de seguir
            // la plataforma móvil anterior.
            SepararDePlataforma();
        }
    }

    // Separa al jugador de la plataforma móvil actual.
    private void SepararDePlataforma()
    {
        // Solo actuamos si el jugador tiene una plataforma registrada.
        if (plataformaActual != null)
        {
            // Guarda la referencia de la plataforma antes de limpiarla.
            PlataformaMovil plataformaAnterior = plataformaActual;

            // Limpia la referencia para evitar repetir la separación.
            plataformaActual = null;

            // Solo cambia la jerarquía si el jugador sigue siendo
            // hijo directo de esa plataforma.
            if (transform.parent == plataformaAnterior.transform)
            {
                // Devuelve al jugador a la jerarquía raíz de la escena.
                // El valor true conserva su posición mundial.
                transform.SetParent(null, true);
            }
        }
    }

    // Se ejecuta cuando Unity desactiva este componente.
    private void OnDisable()
    {
        // No usamos SetParent aquí porque Unity puede estar
        // desactivando el objeto padre y no permite cambiar
        // la jerarquía durante ese proceso.
        // La separación se gestiona desde el movimiento normal.
        plataformaActual = null;
    }
}
