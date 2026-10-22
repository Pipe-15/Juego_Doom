
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    // Indica si el enemigo está en estado de agresividad,
    // es decir, si el jugador se encuentra dentro del rango establecido.
    public bool isAggro;

    // Distancia máxima a la que el enemigo detecta al jugador.
    public float distanceToAggro;

    // Referencia al Transform del jugador.
    // HideInInspector evita que aparezca en el Inspector de Unity.
    [HideInInspector] public Transform playerTransform;

    // Referencia al script que controla los ataques del enemigo.
    private EnemyAttack enemyAttack;

    // Guarda si el jugador estaba dentro del área en el
    // fotograma anterior. Se utiliza para detectar entradas y salidas.
    private bool estabaEnArea = false;

    private void Start()
    {
        // Si no se ha asignado el jugador, intenta encontrarlo
        // mediante el componente PlayerMovement.
        if (playerTransform == null)
        {
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();

            // Si encuentra al jugador, guarda su Transform.
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                // Muestra una advertencia si no encuentra al jugador.
                Debug.LogWarning("No se encontró el jugador.");
            }
        }

        // Obtiene el componente EnemyAttack del mismo objeto.
        enemyAttack = GetComponent<EnemyAttack>();

        // Al comenzar, el enemigo no está en estado de agresividad.
        isAggro = false;
    }

    private void Update()
    {
        // Comprueba continuamente la distancia entre
        // el enemigo y el jugador.
        CheckEnemyAggro();
    }

    // Comprueba si el jugador está dentro del rango de detección.
    public void CheckEnemyAggro()
    {
        // Si no hay una referencia válida al jugador,
        // no puede calcular la distancia.
        if (playerTransform == null)
            return;

        // Calcula la distancia entre el enemigo y el jugador.
        float dis = Vector3.Distance(
            transform.position,
            playerTransform.position
        );

        // Determina si el jugador está dentro del rango de detección.
        bool estaEnArea = dis <= distanceToAggro;

        // Detecta cuándo el jugador entra por primera vez en el área.
        if (estaEnArea && !estabaEnArea)
        {
            Debug.Log("El jugador ha entrado en el área de daño del enemigo.");
        }
        // Detecta cuándo el jugador sale del área.
        else if (!estaEnArea && estabaEnArea)
        {
            Debug.Log("El jugador ha salido del área de daño del enemigo.");
        }

        // Guarda el estado actual para compararlo en el siguiente fotograma.
        estabaEnArea = estaEnArea;

        // Actualiza el estado de agresividad según la distancia.
        isAggro = estaEnArea;
    }

    // Intenta aplicar daño al jugador durante un ataque del enemigo.
    public void EnemyDamage()
    {
        // Comprueba que exista el componente EnemyAttack
        // y que el enemigo esté realizando un ataque.
        if (enemyAttack != null && enemyAttack.isAttacking)
        {
            // Comprueba que exista una referencia al jugador.
            if (playerTransform != null)
            {
                // Busca el componente PlayerStats del jugador.
                PlayerStats stats =
                    playerTransform.GetComponent<PlayerStats>();

                // Si el jugador tiene PlayerStats, le aplica daño.
                if (stats != null)
                {
                    stats.PlayerDamage();
                }
            }
        }
    }
}
