using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public Weapon weapon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GunController player = other.GetComponentInParent<GunController>();

            if (player != null)
            {
                player.EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}