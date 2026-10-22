
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Vida actual del enemigo. Comienza con 100 puntos.
    public int health = 100;

    // Prefabricado del efecto de sangre que aparece cuando
    // el enemigo recibe daño.
    public GameObject Blood;

    // Método que aplica daño al enemigo.
    // damage: cantidad de daño que recibe.
    // rot: rotación utilizada para crear el efecto de sangre.
    public void Damage(int damage, Quaternion rot)
    {
        // Comprueba si existe el administrador de audio
        // antes de reproducir el sonido de daño del enemigo.
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayEnemyDamage();
        }

        // Resta el daño recibido a la vida del enemigo.
        health -= damage;

        // Comprueba si hay un efecto de sangre asignado.
        if (Blood != null)
        {
            // Crea el efecto de sangre en la posición del enemigo,
            // usando la rotación recibida como parámetro.
            GameObject bloodObject = Instantiate(
                Blood,
                transform.position,
                rot
            );

            // Destruye el efecto de sangre después de 0.5 segundos.
            Destroy(bloodObject, 0.5f);
        }

        // Muestra en la consola la vida restante del enemigo.
        Debug.Log("Vida restante del enemigo: " + health);

        // Comprueba si el enemigo se ha quedado sin vida.
        if (health <= 0)
        {
            // Si existe el administrador de enemigos,
            // elimina este enemigo de su registro.
            if (EnemyManager.instance != null)
            {
                EnemyManager.instance.RemoveEnemy(this);
            }

            // Destruye el objeto del enemigo en la escena.
            Destroy(gameObject);
        }
    }
}
