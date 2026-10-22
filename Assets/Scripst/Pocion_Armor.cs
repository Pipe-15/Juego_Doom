using UnityEngine;

public class Pocion_Armor : MonoBehaviour
{
    [Header("Cantidad de armadura que recupera")]
    public int cantidadArmadura = 25;

    private bool recogida = false;

    private void OnTriggerEnter(Collider other)
    {
        if (recogida)
        {
            return;
        }

        PlayerStats jugador =
            other.GetComponentInParent<PlayerStats>();

        if (jugador == null)
        {
            return;
        }

        // No recoger la poción si la armadura ya está al máximo.
        if (jugador.armor >= jugador.maxArmor)
        {
            Debug.Log("Ya tienes la armadura al máximo.");
            return;
        }

        int armaduraAnterior = jugador.armor;

        jugador.RecuperarArmadura(cantidadArmadura);

        if (jugador.armor > armaduraAnterior)
        {
            recogida = true;

            Debug.Log("¡Poción de armadura recogida!");
            Debug.Log("Armadura anterior: " + armaduraAnterior);
            Debug.Log("Armadura actual: " + jugador.armor);

            Destroy(gameObject);
        }
    }
}