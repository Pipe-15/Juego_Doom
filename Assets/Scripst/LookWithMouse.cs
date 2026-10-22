using UnityEngine;

public class LookWithMouse : MonoBehaviour
{
    [Header("Sensibilidad")]
    public float sensibilidad = 2f;          // Qué tan rápido gira la cámara
    public float limiteVertical = 80f;       // Límite de mirar arriba y abajo

    private float rotacionX = 0f;            // Rotación vertical actual
    private Transform jugador;               // Referencia al cuerpo del jugador

    void Start()
    {
        // Bloqueamos el cursor en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Buscamos al jugador por su Tag
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Leemos el movimiento del ratón
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad;

        // Rotación horizontal (izquierda-derecha) → se aplica al jugador
        jugador.Rotate(Vector3.up * mouseX);

        // Rotación vertical (arriba-abajo) → se aplica solo a la cámara
        rotacionX -= mouseY;

        // Limitamos la rotación vertical para que no dé vueltas locas
        rotacionX = Mathf.Clamp(rotacionX, -limiteVertical, limiteVertical);

        // Aplicamos la rotación a la cámara
        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }
}