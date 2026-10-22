using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [Header("Destino")]
    public Transform destino;          // Arrastra aquí el TeleportDestination

    [Header("Opciones")]
    public bool mantenerRotacion = false;  // Si quieres que el jugador mantenga su rotación actual

    private void OnTriggerEnter(Collider other)
    {
        // Comprueba si el objeto que entró es el jugador
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();

            // Si el jugador usa CharacterController, hay que desactivarlo temporalmente
            if (controller != null)
            {
                controller.enabled = false;
            }

            // Teletransportar
            other.transform.position = destino.position;

            // Opcional: también cambiar la rotación
            if (!mantenerRotacion)
            {
                other.transform.rotation = destino.rotation;
            }

            // Volver a activar el CharacterController
            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}
