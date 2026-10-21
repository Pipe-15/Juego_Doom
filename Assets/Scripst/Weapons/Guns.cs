using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nueva Pistola", menuName = "Pistola/Nueva Pistola")]
public class Guns : ScriptableObject
{
    public float range;
    public float horizontalRange;
    public float verticalRange;
    public float fireRate;
    public int damage;
    public AudioClip sound;
}
