
using UnityEngine;

public class PocionArmadura : MonoBehaviour
{
    // Cantidad de armadura que recupera el jugador al recoger la poción.
    public int cantidadArmadura = 25;

    // Se ejecuta cuando otro objeto entra en el área de colisión de la poción.
    private void OnTriggerEnter(Collider other)
    {
        // Muestra en la consola el nombre del objeto que tocó la poción.
        Debug.Log("Poción de armadura: contacto con " + other.name);

        // Busca el componente PlayerStats en el objeto que entró
        // o en alguno de sus objetos padre.
        PlayerStats jugador =
            other.GetComponentInParent<PlayerStats>();

        // Comprueba si el objeto que tocó la poción pertenece al jugador.
        if (jugador != null)
        {
            // Guarda la armadura que tenía el jugador antes de recoger la poción.
            int armaduraAnterior = jugador.armor;

            // Recupera la cantidad de armadura indicada en cantidadArmadura.
            jugador.RecuperarArmadura(cantidadArmadura);

            // Muestra un mensaje confirmando que se recogió la poción.
            Debug.Log("¡POCIÓN DE ARMADURA RECOGIDA!");

            // Muestra la armadura que tenía el jugador antes de recuperarse.
            Debug.Log("Armadura anterior: " + armaduraAnterior);

            // Muestra la armadura que tiene el jugador después de recuperarse.
            Debug.Log("Armadura actual: " + jugador.armor);

            // Destruye el objeto de la poción para que no se pueda recoger otra vez.
            Destroy(gameObject);
        }
        else
        {
            // Advierte en la consola que no se encontró el componente PlayerStats.
            Debug.LogWarning("No se encontró PlayerStats en el jugador.");
        }
    }
}
