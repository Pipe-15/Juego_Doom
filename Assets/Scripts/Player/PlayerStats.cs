
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("VIDA")]
    public int maxHealth = 100;
    public int health = 25;

    [Header("ARMADURA")]
    public int maxArmor = 100;
    public int armor = 0;

    private void Start()
    {
        // Limita los valores iniciales a sus máximos permitidos.
        health = Mathf.Clamp(health, 0, maxHealth);
        armor = Mathf.Clamp(armor, 0, maxArmor);

        Debug.Log("Vida inicial: " + health);
        Debug.Log("Armadura inicial: " + armor);
    }

    public void PlayerDamage()
    {
        if (health > 0)
        {
            health--;

            Debug.Log("Vida actual: " + health);

            if (health == 0)
            {
                Debug.Log("Personaje Muere");
            }
        }
    }

    public void RecuperarVida(int cantidad)
    {
        int vidaAnterior = health;

        health = Mathf.Clamp(health + cantidad, 0, maxHealth);

        Debug.Log("Vida anterior: " + vidaAnterior);
        Debug.Log("Vida actual: " + health);
    }

    public void RecuperarArmadura(int cantidad)
    {
        int armaduraAnterior = armor;

        armor = Mathf.Clamp(armor + cantidad, 0, maxArmor);

        Debug.Log("Armadura anterior: " + armaduraAnterior);
        Debug.Log("Armadura actual: " + armor);
    }
}
