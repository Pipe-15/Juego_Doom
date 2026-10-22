
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Variables relacionadas con la vida del jugador.
    [Header("VIDA")]

    // Cantidad máxima de vida que puede tener el jugador.
    public int maxHealth = 100;

    // Cantidad de vida actual del jugador.
    // Comienza con 25 puntos.
    public int health = 25;

    // Variables relacionadas con la armadura del jugador.
    [Header("ARMADURA")]

    // Cantidad máxima de armadura que puede tener el jugador.
    public int maxArmor = 100;

    // Cantidad de armadura actual del jugador.
    // Comienza sin armadura.
    public int armor = 0;

    private void Start()
    {
        // Limita la vida inicial para que no sea menor que cero
        // ni supere el máximo de vida permitido.
        health = Mathf.Clamp(health, 0, maxHealth);

        // Limita la armadura inicial para que esté entre cero
        // y el máximo de armadura permitido.
        armor = Mathf.Clamp(armor, 0, maxArmor);

        // Muestra la vida inicial del jugador en la consola.
        Debug.Log("Vida inicial: " + health);

        // Muestra la armadura inicial del jugador en la consola.
        Debug.Log("Armadura inicial: " + armor);
    }

    // Reduce la vida del jugador cuando recibe daño.
    public void PlayerDamage()
    {
        // Comprueba que el jugador todavía tenga vida.
        if (health > 0)
        {
            // Reduce la vida en un punto.
            health--;

            // Muestra la vida actual después de recibir daño.
            Debug.Log("Vida actual: " + health);

            // Comprueba si la vida ha llegado a cero.
            if (health == 0)
            {
                // Informa en la consola que el personaje ha muerto.
                Debug.Log("Personaje Muere");
            }
        }
    }

    // Recupera una cantidad determinada de vida.
    // cantidad: puntos de vida que se quieren recuperar.
    public void RecuperarVida(int cantidad)
    {
        // Guarda la vida que tenía el jugador antes de recuperarse.
        int vidaAnterior = health;

        // Suma la cantidad recuperada y limita el resultado
        // para que no supere el máximo ni baje de cero.
        health = Mathf.Clamp(health + cantidad, 0, maxHealth);

        // Muestra la vida que tenía el jugador anteriormente.
        Debug.Log("Vida anterior: " + vidaAnterior);

        // Muestra la vida después de la recuperación.
        Debug.Log("Vida actual: " + health);
    }

    // Recupera una cantidad determinada de armadura.
    // cantidad: puntos de armadura que se quieren recuperar.
    public void RecuperarArmadura(int cantidad)
    {
        // Guarda la armadura que tenía el jugador anteriormente.
        int armaduraAnterior = armor;

        // Suma la armadura recuperada y limita el resultado
        // para que no supere el máximo ni sea menor que cero.
        armor = Mathf.Clamp(armor + cantidad, 0, maxArmor);

        // Muestra la armadura que tenía el jugador antes.
        Debug.Log("Armadura anterior: " + armaduraAnterior);

        // Muestra la armadura después de la recuperación.
        Debug.Log("Armadura actual: " + armor);
    }
}
