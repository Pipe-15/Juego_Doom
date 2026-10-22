using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("VIDA")]
    public int maxHealth = 100;
    public int health = 100;

    [Header("ARMADURA")]
    public int maxArmor = 100;
    public int armor = 0;

    [Header("RESPAWN")]
    public SistemaVidaJugador sistemaVida;

    private void Awake()
    {
        if (sistemaVida == null)
        {
            sistemaVida = GetComponent<SistemaVidaJugador>();
        }
    }

    private void Start()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        armor = Mathf.Clamp(armor, 0, maxArmor);

        Debug.Log("Vida inicial: " + health);
        Debug.Log("Armadura inicial: " + armor);
    }

    // Daño utilizado por los enemigos que ya llaman a PlayerDamage().
    public void PlayerDamage()
    {
        RecibirDaño(1);
    }

    // Permite recibir una cantidad concreta de daño.
    public void RecibirDaño(float cantidad)
    {
        if (cantidad <= 0 || health <= 0)
        {
            return;
        }

        int daño = Mathf.CeilToInt(cantidad);

        health = Mathf.Clamp(health - daño, 0, maxHealth);

        Debug.Log("Vida actual: " + health + "/" + maxHealth);

        if (health <= 0)
        {
            Debug.Log("El jugador ha muerto.");

            if (sistemaVida != null)
            {
                sistemaVida.Morir();
            }
            else
            {
                Debug.LogError(
                    "PlayerStats: falta SistemaVidaJugador en el jugador."
                );
            }
        }
    }

    // Recupera vida sin superar el máximo.
    public void RecuperarVida(int cantidad)
    {
        if (cantidad <= 0)
        {
            return;
        }

        int vidaAnterior = health;

        health = Mathf.Clamp(
            health + cantidad,
            0,
            maxHealth
        );

        Debug.Log("Vida anterior: " + vidaAnterior);
        Debug.Log("Vida actual: " + health);
    }

    // Recupera armadura sin superar el máximo.
    public void RecuperarArmadura(int cantidad)
    {
        if (cantidad <= 0)
        {
            return;
        }

        int armaduraAnterior = armor;

        armor = Mathf.Clamp(
            armor + cantidad,
            0,
            maxArmor
        );

        Debug.Log("Armadura anterior: " + armaduraAnterior);
        Debug.Log("Armadura actual: " + armor);
    }

    // Restaura la vida al reaparecer.
    public void ReiniciarVida()
    {
        health = maxHealth;
    }
}