
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;

    void Start()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;

            Debug.Log("El jugador se encuentra en el área de daño del enemigo.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;

            Debug.Log("El jugador ha salido del área de daño del enemigo.");
        }
    }
}
