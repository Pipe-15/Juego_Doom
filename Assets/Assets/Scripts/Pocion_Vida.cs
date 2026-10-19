
using UnityEngine;

public class PocionVida : MonoBehaviour
{
    // Cantidad de vida que recupera el jugador al recoger la poción.
    public int cantidadVida = 25;

    // Se ejecuta cuando otro objeto entra en el área de colisión de la poción.
    private void OnTriggerEnter(Collider other)
    {
        // Muestra en la consola el nombre del objeto que tocó la poción.
        Debug.Log("Poción de vida: objeto detectado: " + other.name);

        // Busca el componente PlayerStats en el objeto que entró
        // o en alguno de sus objetos padre.
        PlayerStats jugador =
            other.GetComponentInParent<PlayerStats>();

        // Comprueba si el objeto que tocó la poción pertenece al jugador.
        if (jugador != null)
        {
            // Guarda la vida que tenía el jugador antes de recoger la poción.
            int vidaAnterior = jugador.health;

            // Recupera la cantidad de vida indicada en cantidadVida.
            jugador.RecuperarVida(cantidadVida);

            // Muestra un mensaje confirmando que se recogió la poción.
            Debug.Log("¡POCIÓN DE VIDA RECOGIDA!");

            // Muestra la vida que tenía el jugador antes de recuperarse.
            Debug.Log("Vida anterior: " + vidaAnterior);

            // Muestra la vida que tiene el jugador después de recuperarse.
            Debug.Log("Vida actual: " + jugador.health);

            // Destruye el objeto de la poción para que no se pueda recoger otra vez.
            Destroy(gameObject);
        }
        else
        {
            // Informa en la consola si el objeto detectado no tiene PlayerStats.
            Debug.Log("El objeto que entró no tiene PlayerStats.");
        }
    }
}
