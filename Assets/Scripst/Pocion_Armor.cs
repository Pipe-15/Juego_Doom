using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion_Armor : MonoBehaviour
{
    // Cantidad de armadura que recupera el jugador al recoger la poci�n.
    public int cantidadArmadura = 25;

    // Se ejecuta cuando otro objeto entra en el �rea de colisi�n de la poci�n.
    private void OnTriggerEnter(Collider other)
    {
        // Muestra en la consola el nombre del objeto que toc� la poci�n.
        Debug.Log("Poci�n de armadura: contacto con " + other.name);

        // Busca el componente PlayerStats en el objeto que entr�
        // o en alguno de sus objetos padre.
        PlayerStats jugador =
            other.GetComponentInParent<PlayerStats>();

        // Comprueba si el objeto que toc� la poci�n pertenece al jugador.
        if (jugador != null)
        {
            // Guarda la armadura que ten�a el jugador antes de recoger la poci�n.
            int armaduraAnterior = jugador.armor;

            // Recupera la cantidad de armadura indicada en cantidadArmadura.
            jugador.RecuperarArmadura(cantidadArmadura);

            // Muestra un mensaje confirmando que se recogi� la poci�n.
            Debug.Log("�POCI�N DE ARMADURA RECOGIDA!");

            // Muestra la armadura que ten�a el jugador antes de recuperarse.
            Debug.Log("Armadura anterior: " + armaduraAnterior);

            // Muestra la armadura que tiene el jugador despu�s de recuperarse.
            Debug.Log("Armadura actual: " + jugador.armor);

            // Destruye el objeto de la poci�n para que no se pueda recoger otra vez.
            Destroy(gameObject);
        }
        else
        {
            // Advierte en la consola que no se encontr� el componente PlayerStats.
            Debug.LogWarning("No se encontr� PlayerStats en el jugador.");
        }
    }

}
