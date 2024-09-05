using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun_Base : MonoBehaviour
{
    public GunTemplate gun;

    public LayerMask interactLayers;
    public float proximityRadius;

    int bulletsInMagazine;
    bool bulletInChamber = true;
    bool isReloading;

    private void Start()
    {
        bulletsInMagazine = gun.magazineSize;
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(gun.rateOfFire);
        bulletInChamber = true;
    }

    private IEnumerator WaitReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(gun.reloadSpeed);

        int bulletsNeeded = gun.magazineSize - bulletsInMagazine;
        bulletsNeeded = Mathf.Clamp(bulletsNeeded, 0, MainManager.Player.ammo[gun]);

        bulletsInMagazine += bulletsNeeded;
        MainManager.Player.ChangeAmmo(gun, -bulletsNeeded);
        bulletInChamber = true;

        isReloading = false;
    }

    public virtual void Shoot(Transform spawnPos)
    {
        if (!bulletInChamber || isReloading)
            return;

        bulletInChamber = false;
        bulletsInMagazine--;

        ShootRigidbody(spawnPos);
        CheckAmmoState();
    }

    public void CheckAmmoState()
    {
        if (bulletsInMagazine > 0)
        {
            StartCoroutine(FireRate());
        }
        else if (MainManager.Player.ammo[gun] > 0)
        {
            Reload();
        }
    }

    public virtual void ShootRigidbody(Transform spawnPos)
    {
        RaycastHit Hit;

        if (Physics.Raycast(spawnPos.position, spawnPos.forward, out Hit, proximityRadius, interactLayers, QueryTriggerInteraction.Ignore))
        {
            Hit.collider.GetComponent<Object_Base>()?.Damage(gun.damage, Hit.point, Hit.normal);
        }
        else
        {
            Transform bullet = MainManager.Pooling.TakeBullet();
            bullet?.GetComponent<Bullet>().StartBullet(spawnPos.position + spawnPos.forward * proximityRadius, spawnPos.rotation, gun);
        }
    }

    //private void ShootHitscan(Transform spawnPos)
    //{
    //    RaycastHit hit;
    //    Physics.Raycast(spawnPos.position, spawnPos.forward, out hit);

    //    if(hit.collider)
    //        hit.collider.GetComponent<Object_Base>()?.Damage(gun.damage, hit.point, hit.normal);
    //}

    public virtual void Reload()
    {
        if (MainManager.Player.ammo[gun] == 0)
            return;

        StartCoroutine(WaitReload());
    }
}