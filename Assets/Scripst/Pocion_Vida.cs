using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion_Vida : MonoBehaviour
{
    // Cantidad de vida que recupera el jugador al recoger la poci�n.
    public int cantidadVida = 25;

    // Se ejecuta cuando otro objeto entra en el �rea de colisi�n de la poci�n.
    private void OnTriggerEnter(Collider other)
    {
        // Muestra en la consola el nombre del objeto que toc� la poci�n.
        Debug.Log("Poci�n de vida: objeto detectado: " + other.name);

        // Busca el componente PlayerStats en el objeto que entr�
        // o en alguno de sus objetos padre.
        PlayerStats jugador =
            other.GetComponentInParent<PlayerStats>();

        // Comprueba si el objeto que toc� la poci�n pertenece al jugador.
        if (jugador != null)
        {
            // Guarda la vida que ten�a el jugador antes de recoger la poci�n.
            int vidaAnterior = jugador.health;

            // Recupera la cantidad de vida indicada en cantidadVida.
            jugador.RecuperarVida(cantidadVida);

            // Muestra un mensaje confirmando que se recogi� la poci�n.
            Debug.Log("�POCI�N DE VIDA RECOGIDA!");

            // Muestra la vida que ten�a el jugador antes de recuperarse.
            Debug.Log("Vida anterior: " + vidaAnterior);

            // Muestra la vida que tiene el jugador despu�s de recuperarse.
            Debug.Log("Vida actual: " + jugador.health);

            // Destruye el objeto de la poci�n para que no se pueda recoger otra vez.
            Destroy(gameObject);
        }
        else
        {
            // Informa en la consola si el objeto detectado no tiene PlayerStats.
            Debug.Log("El objeto que entr� no tiene PlayerStats.");
        }
    }
}
