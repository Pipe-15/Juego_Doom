using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType
{
    AK47,
    Escopeta
}

public class GunController : MonoBehaviour
{
    public static GunController Instance;

    [Header("Assets de armas")]
    public Guns ak47;
    public Guns escopeta;

    [Header("Municion disponible")]
    public int balasAK = 30;
    public int cartuchosEscopeta = 0;

    [Header("Limite de municion")]
    public int maxBalasAK = 180;
    public int maxCartuchosEscopeta = 50;

    [Header("Deteccion de enemigos")]
    public LayerMask raycastLayerMask;
    public AudioSource audiosource;

    private BoxCollider gunTrigger;
    private bool canFire = true;

    private bool tieneAK47;
    private bool tieneEscopeta;

    // Esta es el arma que se utiliza actualmente.
    public Guns weapon { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;

        gunTrigger = GetComponent<BoxCollider>();

        if (gunTrigger != null)
            gunTrigger.isTrigger = true;

        // El jugador comienza con la AK-47.
        tieneAK47 = ak47 != null;
        tieneEscopeta = false;

        weapon = ak47;
    }

    void Start()
    {
        if (weapon != null)
        {
            SetTrigger();
        }
        else
        {
            Debug.LogError(
                "Asigna el asset Guns de la AK-47 en GunController."
            );
        }
    }

    // Equipar la AK-47.
    public void EquiparAK()
    {
        if (!tieneAK47 || ak47 == null)
            return;

        weapon = ak47;
        SetTrigger();

        Debug.Log("AK-47 equipada.");
    }

    // Equipar la escopeta.
    public void EquiparEscopeta()
    {
        if (escopeta == null)
        {
            Debug.LogError(
                "No has asignado el asset Guns de la escopeta."
            );
            return;
        }

        if (!tieneEscopeta)
        {
            Debug.Log("Primero debes recoger la escopeta del mapa.");
            return;
        }

        weapon = escopeta;
        SetTrigger();

        Debug.Log("Escopeta equipada correctamente.");
    }

    // Recoger un arma del suelo.
    public bool RecogerArma(Guns armaRecogida)
    {
        if (armaRecogida == ak47 && ak47 != null)
        {
            tieneAK47 = true;
            EquiparAK();
            return true;
        }

        if (armaRecogida == escopeta && escopeta != null)
        {
            tieneEscopeta = true;
            EquiparEscopeta();
            return true;
        }

        Debug.LogWarning(
            "El arma recogida no coincide con los assets configurados."
        );

        return false;
    }

    // Configurar el area que detecta a los enemigos.
    public void SetTrigger()
    {
        if (gunTrigger == null || weapon == null)
            return;

        gunTrigger.size = new Vector3(
            weapon.horizontalRange,
            weapon.verticalRange,
            weapon.range
        );

        gunTrigger.center = new Vector3(
            0,
            0.5f * weapon.verticalRange - 1f,
            weapon.range * 0.5f
        );
    }

    // Recoger municion del tipo correspondiente.
    public bool RecogerMunicion(AmmoType tipo, int cantidad)
    {
        if (cantidad <= 0)
            return false;

        if (tipo == AmmoType.AK47)
        {
            int anterior = balasAK;

            balasAK = Mathf.Min(
                balasAK + cantidad,
                maxBalasAK
            );

            Debug.Log("Municion AK-47: " + balasAK);

            return balasAK > anterior;
        }

        if (tipo == AmmoType.Escopeta)
        {
            int anterior = cartuchosEscopeta;

            cartuchosEscopeta = Mathf.Min(
                cartuchosEscopeta + cantidad,
                maxCartuchosEscopeta
            );

            Debug.Log(
                "Municion escopeta: " + cartuchosEscopeta
            );

            return cartuchosEscopeta > anterior;
        }

        return false;
    }

    // Disparar con el arma equipada.
    public void Fire()
    {
        if (!canFire)
            return;

        if (weapon == null)
        {
            Debug.Log("No tienes ningun arma equipada.");
            return;
        }

        // Comprobar y descontar la municion correcta.
        if (weapon == ak47 && tieneAK47)
        {
            if (balasAK <= 0)
            {
                Debug.Log("No quedan balas de AK-47.");
                return;
            }

            balasAK--;
        }
        else if (weapon == escopeta && tieneEscopeta)
        {
            if (cartuchosEscopeta <= 0)
            {
                Debug.Log("No quedan cartuchos de escopeta.");
                return;
            }

            cartuchosEscopeta--;
        }
        else
        {
            return;
        }

        // Cada disparo utiliza el sonido y la cadencia del arma.
        Guns armaDisparada = weapon;

        if (audiosource != null && armaDisparada.sound != null)
        {
            audiosource.PlayOneShot(armaDisparada.sound);
        }

        canFire = false;

        // Buscar a los enemigos dentro del area.
        if (EnemyManager.Instance != null)
        {
            List<Enemy> enemigos = new List<Enemy>(
                EnemyManager.Instance.enemiesInRange
            );

            foreach (Enemy enemy in enemigos)
            {
                if (enemy == null)
                    continue;

                Vector3 direction =
                    (enemy.transform.position - transform.position)
                    .normalized;

                if (Physics.Raycast(
                    transform.position,
                    direction,
                    out RaycastHit hit,
                    armaDisparada.range * 1.5f,
                    raycastLayerMask))
                {
                    // Funciona tambien si el collider esta
                    // en un objeto hijo del enemigo.
                    Enemy enemigoImpactado =
                        hit.collider.GetComponentInParent<Enemy>();

                    if (enemigoImpactado == enemy)
                    {
                        Quaternion rot = Quaternion.LookRotation(
                            -hit.normal
                        );

                        enemy.Damage(armaDisparada.damage, rot);
                    }
                }
            }
        }

        StartCoroutine(EsperarDisparo(armaDisparada.fireRate));
    }

    IEnumerator EsperarDisparo(float tiempo)
    {
        yield return new WaitForSeconds(Mathf.Max(0f, tiempo));
        canFire = true;
    }

    // Detectar enemigos cercanos.
    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null && EnemyManager.Instance != null)
        {
            if (!EnemyManager.Instance.enemiesInRange.Contains(enemy))
            {
                EnemyManager.Instance.AddEnemy(enemy);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Enemy enemy = other.GetComponentInParent<Enemy>();

        if (enemy != null && EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RemoveEnemy(enemy);
        }
    }
}