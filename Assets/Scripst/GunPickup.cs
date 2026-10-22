using UnityEngine;

public class GunPickup : MonoBehaviour
{
    public Guns arma;

    private bool recogida;

    private void OnTriggerEnter(Collider other)
    {
        // Solo recoger cuando toca el personaje,
        // no el Box Collider de deteccion de enemigos.
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

        if (controlador.RecogerArma(arma))
        {
            recogida = true;
            Destroy(gameObject);
        }
    }
}