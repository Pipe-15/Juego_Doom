
using UnityEngine;

public class LavaHazard : MonoBehaviour
{
    [Header("Sonido ambiental de la lava")]

    // Audio que se reproduce continuamente mientras la lava existe.
    public AudioSource sonidoAmbiental;

    [Header("Sonido al tocar la lava")]

    // Efecto de sonido que se reproduce cuando el jugador cae.
    public AudioClip sonidoCaida;

    // Volumen del efecto de caída, entre 0 y 1.
    [Range(0f, 1f)]
    public float volumenCaida = 1f;

    // Evita que el sonido de caída se dispare varias veces
    // si el jugador toca la lava con varios colliders.
    private bool jugadorDetectado = false;

    // Se ejecuta al iniciar el objeto.
    private void Start()
    {
        // Si asignaste un AudioSource para el ambiente,
        // configuramos la reproducción continua de la lava.
        if (sonidoAmbiental != null)
        {
            sonidoAmbiental.loop = true;
            sonidoAmbiental.Play();
        }
    }

    // Se ejecuta cuando otro collider entra en el área
    // de este objeto, siempre que esté configurado como Trigger.
    private void OnTriggerEnter(Collider otro)
    {
        // Muestra en la consola el nombre del objeto detectado.
        Debug.Log("Algo ha tocado la lava: " + otro.name);

        // Busca el sistema de vida en el objeto detectado
        // o en alguno de sus objetos padres.
        SistemaVidaJugador vida =
            otro.GetComponentInParent<SistemaVidaJugador>();

        // Comprueba si el objeto tiene un sistema de vida.
        if (vida != null)
        {
            // Evita procesar varias veces la misma caída
            // mientras este hazard siga detectando al jugador.
            if (jugadorDetectado)
            {
                return;
            }

            // Marcamos que el jugador ya fue detectado.
            jugadorDetectado = true;

            // Informa en la consola que encontró al jugador.
            Debug.Log("¡Jugador detectado! Reproduciendo sonido...");

            // Reproduce el efecto de caída sin necesitar
            // un AudioSource adicional en el objeto de lava.
            if (sonidoCaida != null)
            {
                AudioSource.PlayClipAtPoint(
                    sonidoCaida,
                    transform.position,
                    volumenCaida
                );
            }

            // Conserva tu sistema original de muerte y respawn.
            vida.Morir();

            // Permitimos detectar otra caída después de que el
            // jugador haya salido del área de este hazard.
            // Si Morir() mueve al jugador fuera del trigger,
            // OnTriggerExit restablecerá esta variable.
        }
    }

    // Se ejecuta cuando el collider sale del área de la lava.
    private void OnTriggerExit(Collider otro)
    {
        // Buscamos el sistema de vida del objeto que salió.
        SistemaVidaJugador vida =
            otro.GetComponentInParent<SistemaVidaJugador>();

        // Si el jugador salió del hazard, habilitamos
        // la detección para una futura caída.
        if (vida != null)
        {
            jugadorDetectado = false;
        }
    }
}
