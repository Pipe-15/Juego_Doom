
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public GameObject Blood;

    public void Damage(int damage, Quaternion rot)
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayEnemyDamage();
        }

        health -= damage;

        if (Blood != null)
        {
            GameObject bloodObject = Instantiate(
                Blood,
                transform.position,
                rot
            );

            Destroy(bloodObject, 0.5f);
        }

        Debug.Log("Vida restante del enemigo: " + health);

        if (health <= 0)
        {
            if (EnemyManager.instance != null)
            {
                EnemyManager.instance.RemoveEnemy(this);
            }

            Destroy(gameObject);
        }
    }
}
