using UnityEngine;
using UnityEngine.UI;

public class BarraVidaUI : MonoBehaviour
{
    [Header("Referencias")]
    public Slider barraVida;
    public SistemaVidaJugador sistemaVida;

    void Start()
    {
        if (sistemaVida == null)
        {
            sistemaVida = FindObjectOfType<SistemaVidaJugador>();
        }

        if (barraVida == null)
        {
            barraVida = GetComponent<Slider>();
        }

        if (sistemaVida != null && barraVida != null)
        {
            barraVida.maxValue = sistemaVida.ObtenerVidaMaxima();
            barraVida.value = sistemaVida.ObtenerVidaActual();
        }
    }

    void Update()
    {
        if (sistemaVida != null && barraVida != null)
        {
            barraVida.value = sistemaVida.ObtenerVidaActual();
        }
    }
}