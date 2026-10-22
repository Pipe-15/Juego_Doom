
using UnityEngine;

// Este componente exige que el objeto tenga un Rigidbody.
// El Rigidbody permite mover la plataforma mediante física.
[RequireComponent(typeof(Rigidbody))]
public class PlataformaMovil : MonoBehaviour
{
    // Define los tipos de movimiento que podemos elegir
    // desde el Inspector de Unity.
    public enum TipoMovimiento
    {
        UnSoloEje, // Se mueve en X o Z.
        Vertical,  // Sube y baja por el eje Y.
        Diagonal   // Se mueve combinando los ejes X y Z.
    }

    [Header("Tipo de movimiento")]

    // Permite elegir el comportamiento de esta plataforma
    // sin tener que modificar el código.
    public TipoMovimiento tipoMovimiento =
        TipoMovimiento.UnSoloEje;

    [Header("Dirección del movimiento")]

    // Solo se utiliza cuando el tipo es UnSoloEje.
    // Activado: se mueve en X.
    // Desactivado: se mueve en Z.
    public bool moverEnX = true;

    [Header("Configuración principal")]

    // Distancia total del recorrido de ida y vuelta.
    // Por ejemplo, 6 significa que recorre 6 unidades
    // entre los dos extremos.
    public float distancia = 6f;

    // Velocidad del movimiento principal.
    // Un número mayor hace que la plataforma se mueva más rápido.
    public float velocidad = 2f;

    [Header("Configuración diagonal")]

    // Permite activar o desactivar el movimiento por el eje X
    // cuando utilizamos el tipo Diagonal.
    public bool moverEnXDiagonal = true;

    // Permite activar o desactivar el movimiento por el eje Z
    // cuando utilizamos el tipo Diagonal.
    public bool moverEnZDiagonal = true;

    // Distancia total recorrida por el eje Z en el movimiento diagonal.
    public float distanciaSecundaria = 3f;

    // Velocidad del movimiento secundario.
    // Permite que el eje Z tenga una velocidad diferente al eje X.
    public float velocidadSecundaria = 1.5f;

    [Header("Secuencia")]

    // Tiempo de retraso antes de comenzar el movimiento.
    // Por ejemplo, 2 significa que espera 2 segundos.
    public float retrasoInicio = 0f;

    // Guarda cuánto se ha desplazado la plataforma entre
    // la última actualización y la actual.
    // PlayerMovement utiliza este valor para acompañar
    // el movimiento de la plataforma.
    [HideInInspector]
    public Vector3 deltaMovimiento;

    // Referencia al Rigidbody de la plataforma.
    private Rigidbody rb;

    // Guarda la posición inicial de la plataforma.
    // El movimiento siempre se calcula respecto a esta posición.
    private Vector3 posicionInicial;

    // Controla el tiempo transcurrido para el movimiento principal.
    private float tiempoMovimiento;

    // Controla el tiempo transcurrido para el movimiento secundario.
    private float tiempoSecundario;


    // Awake se ejecuta cuando el objeto se inicializa.
    private void Awake()
    {
        // Obtiene el Rigidbody que tiene esta plataforma.
        rb = GetComponent<Rigidbody>();

        // La plataforma se mueve mediante código,
        // sin que la gravedad la haga caer.
        rb.isKinematic = true;
        rb.useGravity = false;

        // Suaviza visualmente el movimiento del Rigidbody.
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Guarda la posición desde la que comenzará el recorrido.
        posicionInicial = rb.position;
    }


    // FixedUpdate se ejecuta en intervalos regulares
    // y se utiliza para actualizar movimientos de física.
    private void FixedUpdate()
    {
        // Guarda la posición anterior para calcular
        // cuánto se desplazó la plataforma.
        Vector3 posicionAnterior = rb.position;

        // Avanza el tiempo del movimiento principal.
        // Time.fixedDeltaTime hace que el cálculo dependa
        // del intervalo de actualización de la física.
        tiempoMovimiento += Time.fixedDeltaTime * velocidad;

        // Avanza el tiempo del movimiento secundario.
        tiempoSecundario +=
            Time.fixedDeltaTime * velocidadSecundaria;


        // Evita que el tiempo utilizado para mover la plataforma
        // sea negativo mientras se cumple el retraso inicial.
        float tiempo = Mathf.Max(
            0f,
            tiempoMovimiento - retrasoInicio
        );


        // Calcula un desplazamiento que va de un extremo al otro
        // y después regresa, gracias a Mathf.PingPong.
        // Restar la mitad de la distancia centra el recorrido
        // respecto a la posición inicial.
        float desplazamiento =
            Mathf.PingPong(tiempo, distancia) - distancia / 2f;


        // Comienza calculando la nueva posición desde
        // la posición inicial guardada anteriormente.
        Vector3 nuevaPosicion = posicionInicial;


        // Comprueba qué tipo de movimiento elegimos
        // en el Inspector de Unity.
        switch (tipoMovimiento)
        {
            // OPCIÓN 1: movimiento en un solo eje.
            case TipoMovimiento.UnSoloEje:

                // Si moverEnX está activado, se desplaza en X.
                if (moverEnX)
                {
                    nuevaPosicion.x += desplazamiento;
                }
                // Si está desactivado, se desplaza en Z.
                else
                {
                    nuevaPosicion.z += desplazamiento;
                }

                break;


            // OPCIÓN 2: movimiento vertical.
            case TipoMovimiento.Vertical:

                // Modifica la altura de la plataforma.
                // El eje Y representa la altura en Unity.
                nuevaPosicion.y += desplazamiento;

                break;


            // OPCIÓN 3: movimiento diagonal.
            case TipoMovimiento.Diagonal:

                // Comprueba si el eje X está habilitado.
                if (moverEnXDiagonal)
                {
                    nuevaPosicion.x += desplazamiento;
                }

                // Comprueba si el eje Z está habilitado.
                if (moverEnZDiagonal)
                {
                    // Calcula el desplazamiento secundario.
                    // Tiene su propia velocidad y distancia.
                    float desplazamientoZ =
                        Mathf.PingPong(
                            Mathf.Max(
                                0f,
                                tiempoSecundario - retrasoInicio
                            ),
                            distanciaSecundaria
                        ) - distanciaSecundaria / 2f;

                    // Añade el desplazamiento calculado
                    // a la posición del eje Z.
                    nuevaPosicion.z += desplazamientoZ;
                }

                break;
        }


        // Mueve el Rigidbody hasta la nueva posición calculada.
        rb.MovePosition(nuevaPosicion);


        // Calcula la diferencia entre la posición nueva
        // y la anterior.
        // PlayerMovement utiliza este desplazamiento para
        // mover al jugador junto con la plataforma.
        deltaMovimiento = nuevaPosicion - posicionAnterior;
    }
}
