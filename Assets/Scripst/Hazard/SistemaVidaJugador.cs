
using UnityEngine;

public class SistemaVidaJugador : MonoBehaviour
{
    [Header("Configuración de vida")]

    // Cantidad máxima de vida que puede tener el jugador.
    public float vidaMaxima = 100f;

    // Vida actual del jugador.
    // SerializeField permite verla y editarla desde el Inspector,
    // aunque la variable sea privada.
    [SerializeField]
    private float vidaActual;

    [Header("Respawn del jugador")]

    // Referencia al script que devuelve al jugador
    // a su punto de reaparición.
    public RespawnJugador sistemaRespawn;


    // Se ejecuta al inicializar el objeto.
    private void Awake()
    {
        // Al comenzar, el jugador tiene toda su vida.
        vidaActual = vidaMaxima;

        // Si no se asignó el sistema de respawn desde el Inspector,
        // intenta encontrarlo en el mismo objeto del jugador.
        if (sistemaRespawn == null)
        {
            sistemaRespawn = GetComponent<RespawnJugador>();
        }
    }


    // Reduce la vida del jugador en la cantidad indicada.
    public void RecibirDaño(float cantidad)
    {
        // Evita aplicar daño negativo o igual a cero.
        if (cantidad <= 0f)
        {
            return;
        }

        // Resta la cantidad de daño a la vida actual.
        vidaActual -= cantidad;

        // Mantiene la vida entre cero y la vida máxima.
        vidaActual = Mathf.Clamp(
            vidaActual,
            0f,
            vidaMaxima
        );

        // Muestra la vida actual en la consola de Unity.
        Debug.Log(
            "Vida del jugador: " +
            vidaActual + "/" + vidaMaxima
        );

        // Si la vida llega a cero, el jugador muere.
        if (vidaActual <= 0f)
        {
            Morir();
        }
    }


    // Ejecuta las acciones necesarias cuando el jugador muere.
    public void Morir()
    {
        // Restaura la vida al máximo.
        vidaActual = vidaMaxima;

        // Comprueba que exista un sistema de respawn.
        if (sistemaRespawn != null)
        {
            // Devuelve al jugador al punto configurado.
            sistemaRespawn.Reaparecer();
        }
        else
        {
            // Muestra un error si falta el componente necesario.
            Debug.LogError(
                "SistemaVidaJugador: falta el componente RespawnJugador."
            );
        }
    }


    // Restaura la vida sin mover al jugador.
    // Puede utilizarse cuando se reinicia una partida.
    public void ReiniciarVida()
    {
        vidaActual = vidaMaxima;
    }


    // Devuelve la vida actual para que otros scripts
    // puedan consultarla, por ejemplo, una barra de vida.
    public float ObtenerVidaActual()
    {
        return vidaActual;
    }


    // Devuelve la vida máxima para que otros scripts
    // puedan calcular el porcentaje de vida restante.
    public float ObtenerVidaMaxima()
    {
        return vidaMaxima;
    }
}
