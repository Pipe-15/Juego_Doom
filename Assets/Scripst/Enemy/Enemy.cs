using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public GameObject blood;
    // Update is called once per frame
    void Update()
    {
        EnemyDead();
    }

    public void Damage(int damage, Quaternion rot)
    {
        health -= damage;
        GameObject bloodObject = Instantiate(blood, transform.position, rot);
        Destroy(bloodObject, 0.5f);
    }

    public void EnemyDead()
    {
        if (health <= 0)
        {
            // sacar lista
            Destroy(gameObject);
        }
    }

}
