using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [Header("Tipo de municion")]
    public AmmoType tipo;

    [Header("Cantidad")]
    public int cantidad = 0;

    private bool recogida;

    private void OnTriggerEnter(Collider other)
    {
        if (!(other is CharacterController))
            return;

        if (recogida)
            return;

        GunController controlador =
            other.GetComponent<GunController>();

        if (controlador == null)
            controlador = other.GetComponentInParent<GunController>();

        if (controlador == null)
            return;

        // Si la cantidad queda en cero,
        // se usa la cantidad predeterminada del tipo.
        int cantidadReal = cantidad > 0
            ? cantidad
            : (tipo == AmmoType.AK47 ? 30 : 5);

        if (controlador.RecogerMunicion(tipo, cantidadReal))
        {
            recogida = true;
            Destroy(gameObject);
        }
    }
}