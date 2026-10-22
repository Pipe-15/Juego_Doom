
using UnityEngine;

// Controla la agresividad del enemigo y el daño al jugador.
public class EnemyAggro : MonoBehaviour
{
    [Header("Detección del jugador")]

    // Indica si el enemigo está dentro del rango de agresividad.
    public bool isAggro;

    // Distancia máxima a la que el enemigo se activa.
    public float distanceToAggro = 10f;

    // Referencia al jugador.
    [HideInInspector]
    public Transform playerTransform;

    [Header("Daño al jugador")]

    // Cantidad de vida que quita cada ataque.
    public float daño = 10f;

    // Tiempo entre ataques consecutivos.
    public float tiempoEntreAtaques = 1f;

    // Momento en que se permite el siguiente ataque.
    private float siguienteAtaque;

    // Sistema de vida del jugador.
    private SistemaVidaJugador sistemaVida;

    // Componente que detecta si el jugador está en el área de ataque.
    private EnemyAttack enemyAttack;

    // Se ejecuta al iniciar el enemigo.
    private void Start()
    {
        // Al principio, el enemigo no está agresivo.
        isAggro = false;

        // Busca al jugador si no se asignó manualmente.
        PlayerMovement jugador = FindAnyObjectByType<PlayerMovement>();

        // Comprueba que el jugador exista antes de guardar su referencia.
        if (jugador != null)
        {
            playerTransform = jugador.transform;

            // Busca el sistema de vida del jugador.
            sistemaVida =
                jugador.GetComponentInParent<SistemaVidaJugador>();
        }

        // Busca EnemyAttack en el enemigo o en sus hijos.
        enemyAttack = GetComponentInChildren<EnemyAttack>();
    }

    // Se ejecuta una vez por fotograma.
    private void Update()
    {
        // Si no encontramos al jugador, no continuamos.
        if (playerTransform == null)
        {
            isAggro = false;
            return;
        }

        // Comprueba la distancia entre el ENEMIGO y el JUGADOR.
        // Esta es la corrección del error de tu código anterior.
        float distancia = Vector3.Distance(
            transform.position,
            playerTransform.position
        );

        // Activa o desactiva la agresividad según la distancia.
        isAggro = distancia <= distanceToAggro;

        // Comprueba que el enemigo esté atacando y que
        // tenga una referencia válida al sistema de vida.
        if (isAggro &&
            enemyAttack != null &&
            enemyAttack.isAttacking &&
            sistemaVida != null)
        {
            // Evita quitar vida en todos los fotogramas.
            if (Time.time >= siguienteAtaque)
            {
                // Quita vida al jugador.
                sistemaVida.RecibirDaño(daño);

                // Programa el siguiente ataque.
                siguienteAtaque = Time.time + tiempoEntreAtaques;

                // Informa del daño aplicado.
                Debug.Log("El enemigo quitó " + daño + " puntos de vida.");
            }
        }
    }

    // Este método puede seguir utilizándose desde una animación
    // si tu enemigo ya tenía un evento de ataque configurado.
    public void EnemyDamage()
    {
        // El daño se gestiona en Update para controlar
        // la distancia y el tiempo entre ataques.
        // No aplicamos daño aquí para evitar duplicarlo.
    }
}
