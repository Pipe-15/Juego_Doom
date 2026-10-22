
using UnityEngine;

// Detecta si el jugador está dentro del área de ataque.
public class EnemyAttack : MonoBehaviour
{
    // Indica si el jugador está dentro del área de daño.
    public bool isAttacking = false;

    // Se ejecuta cuando un collider entra en el área Trigger.
    private void OnTriggerEnter(Collider other)
    {
        // Busca el sistema de vida en el objeto detectado
        // o en alguno de sus objetos padres.
        SistemaVidaJugador vida =
            other.GetComponentInParent<SistemaVidaJugador>();

        // Solo detectamos como objetivo a un objeto que tenga
        // el sistema de vida del jugador.
        if (vida != null)
        {
            // Indica que el jugador puede recibir ataques.
            isAttacking = true;

            // Muestra un mensaje en la consola.
            Debug.Log("El jugador entró en el área de ataque.");
        }
    }

    // Se ejecuta cuando un collider sale del área Trigger.
    private void OnTriggerExit(Collider other)
    {
        // Comprueba si el objeto que salió tiene el sistema de vida.
        SistemaVidaJugador vida =
            other.GetComponentInParent<SistemaVidaJugador>();

        // Si es el jugador, deja de indicar que está siendo atacado.
        if (vida != null)
        {
            isAttacking = false;

            // Muestra un mensaje en la consola.
            Debug.Log("El jugador salió del área de ataque.");
        }
    }
}
