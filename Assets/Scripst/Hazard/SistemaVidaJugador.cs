
using UnityEngine;

public class SistemaVidaJugador : MonoBehaviour
{
    [Header("Referencias del jugador")]
    public PlayerStats jugador;
    public RespawnJugador sistemaRespawn;

    private bool procesandoMuerte = false;

    private void Awake()
    {
        if (jugador == null)
        {
            jugador = GetComponent<PlayerStats>();
        }

        if (sistemaRespawn == null)
        {
            sistemaRespawn = GetComponent<RespawnJugador>();
        }
    }

    // Permite que otros scripts soliciten daño a este sistema.
    public void RecibirDaño(float cantidad)
    {
        if (jugador == null)
        {
            Debug.LogError(
                "SistemaVidaJugador: falta PlayerStats."
            );
            return;
        }

        jugador.RecibirDaño(cantidad);
    }

    // Ejecuta la muerte y la reaparición una sola vez.
    public void Morir()
    {
        if (procesandoMuerte)
        {
            return;
        }

        procesandoMuerte = true;

        if (jugador != null)
        {
            jugador.ReiniciarVida();
        }
        else
        {
            Debug.LogError(
                "SistemaVidaJugador: falta PlayerStats."
            );
        }

        if (sistemaRespawn != null)
        {
            sistemaRespawn.Reaparecer();
        }
        else
        {
            Debug.LogError(
                "SistemaVidaJugador: falta RespawnJugador."
            );
        }

        procesandoMuerte = false;
    }

    // Permite reiniciar la vida desde otros scripts.
    public void ReiniciarVida()
    {
        if (jugador != null)
        {
            jugador.ReiniciarVida();
        }
    }

    public float ObtenerVidaActual()
    {
        return jugador != null ? jugador.health : 0f;
    }

    public float ObtenerVidaMaxima()
    {
        return jugador != null ? jugador.maxHealth : 0f;
    }
}

