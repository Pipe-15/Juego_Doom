using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="New Weapon", menuName = "Weapon/New Weapon")]

public class Weapon : ScriptableObject
{
  
    public float range;
    public float verticalRange;
    public float horizontalRange;
    public int damage;
    public float fireRate;
    public AudioClip sound;
    public GameObject weaponPrefab;
}
