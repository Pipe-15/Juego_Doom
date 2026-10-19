
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public static GunController instance;

    public Weapon gun;
    public LayerMask raycastLayerMask;
    public AudioSource audioSource;

    private bool canFire = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        // Disparar al presionar el clic izquierdo
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon == null)
            return;

        gun = newWeapon;

        Debug.Log("Arma equipada: " + gun.name);
    }

    public void Fire()
    {
        if (!canFire || gun == null)
            return;

        Camera cam = Camera.main;

        if (cam == null)
        {
            Debug.LogWarning("No se encontró una cámara con el tag MainCamera.");
            return;
        }

        // Sonido del disparo
        if (audioSource != null && gun.sound != null)
        {
            audioSource.PlayOneShot(gun.sound);
        }

        // Raycast desde el centro de la cámara
        Ray ray = cam.ViewportPointToRay(
            new Vector3(0.5f, 0.5f, 0f)
        );

        RaycastHit hit;

        if (Physics.Raycast(
            ray,
            out hit,
            gun.range,
            raycastLayerMask,
            QueryTriggerInteraction.Ignore))
        {
            Debug.DrawRay(
                ray.origin,
                ray.direction * hit.distance,
                Color.red,
                1f
            );

            // Buscar el componente Enemy en el objeto golpeado
            // o en alguno de sus padres
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy != null)
            {
                Quaternion rot = Quaternion.LookRotation(-hit.normal);
                enemy.Damage(gun.damage, rot);

                Debug.Log("Enemigo golpeado. Daño: " + gun.damage);
            }
            else
            {
                Debug.Log("Impacto contra: " + hit.collider.name);
            }
        }
        else
        {
            Debug.DrawRay(
                ray.origin,
                ray.direction * gun.range,
                Color.green,
                1f
            );
        }

        StartCoroutine(EsperarDisparo(gun.fireRate));
    }

    private IEnumerator EsperarDisparo(float tiempo)
    {
        canFire = false;

        yield return new WaitForSeconds(tiempo);

        canFire = true;
    }
}
