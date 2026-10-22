
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    // Indica si el enemigo está atacando al jugador.
    // Es true cuando el jugador entra en el área de ataque
    // y false cuando sale de ella.
    public bool isAttacking;

    private void Start()
    {
        // Al iniciar el juego, el enemigo no está atacando.
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entra en el área
        // de ataque tiene la etiqueta Player.
        if (other.CompareTag("Player"))
        {
            // Activa el estado de ataque cuando el jugador entra
            // en el área de detección del enemigo.
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Comprueba si el jugador sale del área de ataque.
        if (other.CompareTag("Player"))
        {
            // Desactiva el estado de ataque cuando el jugador
            // abandona el área de detección del enemigo.
            isAttacking = false;
        }
    }
}
