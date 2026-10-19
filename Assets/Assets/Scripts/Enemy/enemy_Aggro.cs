
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    public bool isAggro;
    public float distanceToAggro;

    [HideInInspector] public Transform playerTransform;

    private EnemyAttack enemyAttack;
    private bool estabaEnArea = false;

    private void Start()
    {
        if (playerTransform == null)
        {
            PlayerMovement player = FindAnyObjectByType<PlayerMovement>();

            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró el jugador.");
            }
        }

        enemyAttack = GetComponent<EnemyAttack>();
        isAggro = false;
    }

    private void Update()
    {
        CheckEnemyAggro();
    }

    public void CheckEnemyAggro()
    {
        if (playerTransform == null)
            return;

        float dis = Vector3.Distance(
            transform.position,
            playerTransform.position
        );

        bool estaEnArea = dis <= distanceToAggro;

        if (estaEnArea && !estabaEnArea)
        {
            Debug.Log("El jugador ha entrado en el área de daño del enemigo.");
        }
        else if (!estaEnArea && estabaEnArea)
        {
            Debug.Log("El jugador ha salido del área de daño del enemigo.");
        }

        estabaEnArea = estaEnArea;
        isAggro = estaEnArea;
    }

    public void EnemyDamage()
    {
        if (enemyAttack != null && enemyAttack.isAttacking)
        {
            if (playerTransform != null)
            {
                PlayerStats stats =
                    playerTransform.GetComponent<PlayerStats>();

                if (stats != null)
                {
                    stats.PlayerDamage();
                }
            }
        }
    }
}
