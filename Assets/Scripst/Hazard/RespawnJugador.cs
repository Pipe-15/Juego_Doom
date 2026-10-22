
using UnityEngine;

public class RespawnJugador : MonoBehaviour
{
    [Header("Punto al que vuelve el jugador")]
    public Transform puntoRespawn;

    private CharacterController controlador;

    private void Awake()
    {
        controlador = GetComponent<CharacterController>();
    }

    public void Reaparecer()
    {
        if (puntoRespawn == null)
        {
            Debug.LogError(
                "RespawnJugador: falta asignar el Punto Respawn."
            );
            return;
        }

        if (controlador != null)
        {
            controlador.enabled = false;
        }

        transform.SetPositionAndRotation(
            puntoRespawn.position,
            puntoRespawn.rotation
        );

        if (controlador != null)
        {
            controlador.enabled = true;
        }

        Debug.Log("Jugador reaparecido en el punto de respawn.");
    }
}
