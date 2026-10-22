
using UnityEngine;
using UnityEngine.AI;

// Controla la patrulla y la persecución del enemigo.
public class EnemyAI : MonoBehaviour
{
    [Header("Puntos de patrulla")]

    // Lugares por los que caminará el enemigo.
    // Debes asignarlos desde el Inspector.
    public Transform[] points;

    // Índice del siguiente punto de patrulla.
    private int destPoint = 0;

    [Header("Referencias")]

    // Componente que determina si el jugador está cerca.
    private EnemyAggro enemyAggro;

    // Referencia a la posición del jugador.
    private Transform playerTransform;

    // Componente que permite desplazarse por el NavMesh.
    private NavMeshAgent navMeshAgent;

    [Header("Velocidades")]

    // Velocidad al caminar durante las patrullas.
    public float patrolSpeed = 2.5f;

    // Velocidad al perseguir al jugador.
    // Un valor mayor hace que el enemigo corra más rápido.
    public float chaseSpeed = 7f;

    [Header("Persecución")]

    // Distancia a la que el enemigo se detendrá del jugador.
    public float chaseStoppingDistance = 1.5f;

    // Indica si el enemigo estaba persiguiendo al jugador
    // durante el fotograma anterior.
    private bool estabaPersiguiendo = false;

    // Obtiene los componentes al inicializar el objeto.
    private void Awake()
    {
        // Busca el componente NavMeshAgent en el enemigo.
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Busca el sistema de agresividad en el mismo objeto.
        enemyAggro = GetComponent<EnemyAggro>();
    }

    // Se ejecuta al iniciar el enemigo.
    private void Start()
    {
        // Comprueba que exista el agente de navegación.
        if (navMeshAgent == null)
        {
            Debug.LogError(
                "El enemigo necesita un componente NavMeshAgent.",
                this
            );
            enabled = false;
            return;
        }

        // Comprueba que exista el sistema de agresividad.
        if (enemyAggro == null)
        {
            Debug.LogError(
                "El enemigo necesita el componente EnemyAggro.",
                this
            );
            enabled = false;
            return;
        }

        // Busca al jugador si no se asignó manualmente.
        PlayerMovement jugador = FindAnyObjectByType<PlayerMovement>();

        // Guarda la referencia solo si encontró al jugador.
        if (jugador != null)
        {
            playerTransform = jugador.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador.", this);
        }

        // Comprueba que el agente esté colocado sobre el NavMesh.
        if (!navMeshAgent.isOnNavMesh)
        {
            Debug.LogWarning(
                "El enemigo está fuera del NavMesh.",
                this
            );
            return;
        }

        // Configura la velocidad inicial de patrulla.
        navMeshAgent.speed = patrolSpeed;

        // Empieza a caminar hacia el primer punto de patrulla.
        NextPoint();
    }

    // Selecciona el siguiente punto de patrulla.
    private void NextPoint()
    {
        // Comprueba que existan puntos de patrulla.
        if (points == null || points.Length == 0)
        {
            return;
        }

        // Evita errores si el punto seleccionado está vacío.
        if (points[destPoint] == null)
        {
            // Busca el siguiente punto disponible.
            for (int i = 0; i < points.Length; i++)
            {
                destPoint = (destPoint + 1) % points.Length;

                if (points[destPoint] != null)
                {
                    break;
                }
            }

            // Si no hay ningún punto válido, no continúa.
            if (points[destPoint] == null)
            {
                return;
            }
        }

        // Ordena al enemigo caminar hacia el punto seleccionado.
        navMeshAgent.SetDestination(points[destPoint].position);

        // Prepara el índice para la siguiente patrulla.
        destPoint = (destPoint + 1) % points.Length;
    }

    // Se ejecuta una vez por fotograma.
    private void Update()
    {
        // No intenta mover al enemigo si falta alguna referencia.
        if (navMeshAgent == null ||
            enemyAggro == null ||
            !navMeshAgent.isOnNavMesh)
        {
            return;
        }

        // Actualiza el comportamiento de movimiento.
        EnemyMovement();
    }

    // Decide si el enemigo debe perseguir o patrullar.
    private void EnemyMovement()
    {
        // Comprueba si el jugador sigue existiendo.
        if (playerTransform == null)
        {
            // Busca nuevamente al jugador si fue reemplazado.
            PlayerMovement jugador =
                FindAnyObjectByType<PlayerMovement>();

            if (jugador != null)
            {
                playerTransform = jugador.transform;
            }
            else
            {
                // Si no existe el jugador, intenta seguir patrullando.
                VolverAPatrullar();
                return;
            }
        }

        // Si el jugador está dentro del rango de agresividad,
        // el enemigo inicia o continúa la persecución.
        if (enemyAggro.isAggro)
        {
            // Marca que el enemigo está persiguiendo.
            estabaPersiguiendo = true;

            // Aumenta la velocidad durante la persecución.
            navMeshAgent.speed = chaseSpeed;

            // Permite que se acerque más al jugador.
            navMeshAgent.stoppingDistance = chaseStoppingDistance;

            // Actualiza continuamente el destino para seguir
            // al jugador incluso cuando cambia de posición.
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else
        {
            // Si acaba de perder al jugador, retoma la patrulla
            // inmediatamente en lugar de quedarse en su destino anterior.
            if (estabaPersiguiendo)
            {
                VolverAPatrullar();
                return;
            }

            // Mantiene la velocidad normal de patrulla.
            navMeshAgent.speed = patrolSpeed;

            // Permite que llegue hasta cada punto de patrulla.
            navMeshAgent.stoppingDistance = 0f;

            // Cuando llega a un punto, selecciona el siguiente.
            if (!navMeshAgent.pathPending &&
                navMeshAgent.remainingDistance <= 0.5f)
            {
                NextPoint();
            }
        }
    }

    // Detiene la persecución y reanuda la patrulla.
    private void VolverAPatrullar()
    {
        // Indica que el enemigo ya no está persiguiendo.
        estabaPersiguiendo = false;

        // Recupera la velocidad de patrulla.
        navMeshAgent.speed = patrolSpeed;

        // Restablece la distancia de parada.
        navMeshAgent.stoppingDistance = 0f;

        // Selecciona un punto de patrulla como nuevo destino.
        NextPoint();
    }
}
