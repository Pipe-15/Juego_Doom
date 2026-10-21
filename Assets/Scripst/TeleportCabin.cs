using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCabin : MonoBehaviour
{
    // Transform del portal al que se teletransportar� el jugador.
    // Se asigna desde el Inspector de Unity.
    [Header("Destino")]
    public Transform destino;

    // CanvasGroup que controla la pantalla negra de transici�n.
    [Header("Transicion de pantalla")]
    public CanvasGroup pantallaNegra;

    // Duraci�n del fundido de entrada y salida de la pantalla negra.
    public float duracionTransicion = 0.2f;

    // Tiempo que la pantalla permanece completamente negra.
    public float tiempoOscuro = 0.15f;

    // Tiempo durante el cual se bloquean nuevos teletransportes.
    [Header("Regreso")]
    public float tiempoBloqueo = 0.8f;

    // Evita que este portal inicie otro teletransporte mientras
    // el anterior todav�a est� en proceso.
    private bool teletransportando = false;

    // Bloqueo compartido por todos los objetos que usan este script.
    // Evita que el jugador active inmediatamente el portal de llegada.
    private static float bloquearHasta = 0f;

    private void Start()
    {
        // Al comenzar el juego, la pantalla debe ser transparente.
        if (pantallaNegra != null)
        {
            pantallaNegra.alpha = 0f;

            // Evita que el panel intercepte clics de la interfaz.
            pantallaNegra.blocksRaycasts = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si ya se est� teletransportando o sigue activo el bloqueo,
        // no se inicia otro viaje.
        if (teletransportando || Time.time < bloquearHasta)
            return;

        // Obtiene el objeto ra�z del collider que entr� al portal.
        // Esto permite identificar al jugador aunque tenga colliders
        // en objetos hijos.
        Transform jugador = other.transform.root;

        // Solo se permite teletransportar objetos con el tag Player.
        if (!jugador.CompareTag("Player"))
            return;

        // Comprueba que se haya asignado un portal de destino.
        if (destino == null)
        {
            Debug.LogWarning(
                "No has asignado el destino de la cabina."
            );
            return;
        }

        // Inicia la secuencia de teletransporte.
        StartCoroutine(Teletransportar(jugador));
    }

    private IEnumerator Teletransportar(Transform jugador)
    {
        // Marca el proceso como activo para evitar activaciones repetidas.
        teletransportando = true;

        // Busca el CharacterController del jugador.
        // Se desactivar� temporalmente para moverlo sin conflictos.
        CharacterController controlador =
            jugador.GetComponent<CharacterController>();

        // Oscurece la pantalla antes de mover al jugador.
        if (pantallaNegra != null)
            yield return StartCoroutine(FundirPantalla(0f, 1f));

        // Desactiva temporalmente las colisiones del CharacterController.
        if (controlador != null)
            controlador.enabled = false;

        // Mueve al jugador a la posici�n y rotaci�n del portal de destino.
        jugador.SetPositionAndRotation(
            destino.position,
            destino.rotation
        );

        // Actualiza las posiciones de los objetos en el sistema de f�sicas.
        Physics.SyncTransforms();

        // Reactiva el controlador despu�s del movimiento.
        if (controlador != null)
            controlador.enabled = true;

        // Establece el momento hasta el que no se permitir�n
        // nuevas activaciones de los portales.
        bloquearHasta = Time.time + tiempoBloqueo;

        // Mantiene la pantalla negra durante el tiempo configurado.
        yield return new WaitForSeconds(tiempoOscuro);

        // Devuelve la imagen del juego gradualmente.
        if (pantallaNegra != null)
            yield return StartCoroutine(FundirPantalla(1f, 0f));

        // Permite volver a iniciar el proceso desde este portal.
        teletransportando = false;
    }

    private IEnumerator FundirPantalla(float inicio, float fin)
    {
        // Tiempo transcurrido desde el inicio del fundido.
        float tiempo = 0f;

        // Establece la transparencia inicial.
        pantallaNegra.alpha = inicio;

        // Cambia gradualmente la transparencia hasta alcanzar el valor final.
        while (tiempo < duracionTransicion)
        {
            tiempo += Time.deltaTime;

            // Interpola entre la transparencia inicial y la final.
            pantallaNegra.alpha = Mathf.Lerp(
                inicio,
                fin,
                tiempo / duracionTransicion
            );

            // Espera al siguiente fotograma para continuar el fundido.
            yield return null;
        }

        // Garantiza que la transparencia termine en el valor exacto.
        pantallaNegra.alpha = fin;
    }
}
