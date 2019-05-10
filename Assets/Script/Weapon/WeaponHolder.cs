using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("WeaponDataBase")]
    public WeaponAssautScriptable weapon;

    [Header("WeaponSetting")]
    public Transform pointEject;
    private float nextTimeToFire = 0f;
    private bool isReloading = false;

    [Header("UI Weapon")]
    public GameObject WeaponOverlay;
    public Text NameWeapon;
    public Text AmmoWeapon;


    private void Start()
    {
        if (weapon == null)
            WeaponOverlay.gameObject.SetActive(false);

        weapon.currentAmmo = weapon.maxAmmo;
    }


    private void Update()
    {
        if (weapon == null || isReloading)
            return;

        if (weapon.currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        switch (weapon.typeFire)
        {
            case WeaponAssautScriptable.TypeFire.Auto:
                if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
                {
                    Shoot();
                    nextTimeToFire = Time.time + 1f / weapon.fireRate;
                }

                break;
            case WeaponAssautScriptable.TypeFire.Semi:

                if (Input.GetButtonDown("Fire1"))
                    Shoot();

                break;
        }

        ShowInfos();
    }



    void Shoot()
    {
        RaycastHit hit;
        weapon.currentAmmo--;

        if (weapon.currentAmmo > 0)
        {
            if (Physics.Raycast(pointEject.transform.position, pointEject.transform.forward, out hit, weapon.range))
            {
                EnemyIA enemy = hit.transform.GetComponent<EnemyIA>();

                if (enemy != null)
                    enemy.TakeDamage(weapon.damage);

                //crée un object et le detruite au boue de 2s
                GameObject ImpactGO = (GameObject)Instantiate(weapon.ImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(ImpactGO, 2f);

            }

            GameObject MuzzulflashGO = (GameObject)Instantiate(weapon.MuzzleFlash, pointEject.position, pointEject.rotation);
            Destroy(MuzzulflashGO, 0.5f);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(weapon.reloadTime);

        weapon.currentAmmo = weapon.maxAmmo;
        isReloading = false;
    }

    void ShowInfos()
    {

        NameWeapon.text = weapon.nameWeapon;
        AmmoWeapon.text = weapon.currentAmmo + " / " + weapon.maxAmmo;

    }
}
