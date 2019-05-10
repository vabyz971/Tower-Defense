using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon", menuName ="Weapons/Assaut" )]
public class WeaponAssautScriptable : ScriptableObject
{

    public string nameWeapon = "NameWeapon";
    public string description;

    [Header("TypeFire")]
    public TypeFire typeFire;
    public float fireRate;
    public enum TypeFire { Auto, Semi}

    [Header("Damage")]
    public float damage;
    public float range;

    [Header("Munition")]
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime = 1f;

    [Header("Effect")]
    public GameObject MuzzleFlash;
    public GameObject ImpactEffect;



}
