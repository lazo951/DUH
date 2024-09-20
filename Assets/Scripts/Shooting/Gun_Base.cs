using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Gun_Base : MonoBehaviour
{
    public GunTemplate gun;

    int bulletsInMagazine;
    bool bulletInChamber = true;
    bool isReloading;

    AudioSource gunAudio;

    private void Start()
    {
        bulletsInMagazine = gun.magazineSize;
        gunAudio = GetComponent<AudioSource>();
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(gun.rateOfFire);
        bulletInChamber = true;
    }

    private IEnumerator WaitReload()
    {
        PlaySoundEffect(gun.soundReload);

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
        {
            if (MainManager.Player.ammo[gun] < 1 && !gunAudio.isPlaying)
                PlaySoundEffect(gun.soundEmpty);

            return;
        }

        bulletInChamber = false;
        bulletsInMagazine--;

        CheckProximity(spawnPos);
        CheckAmmoState();

        MainManager.Effects.AnimateCrosshair();
        gunAudio.Stop();
        PlaySoundEffect(gun.soundShooting[Random.Range(0, gun.soundShooting.Length)]);
    }

    public virtual void CheckProximity(Transform spawnPos)
    {
        RaycastHit Hit;

        if (Physics.Raycast(spawnPos.position, spawnPos.forward, out Hit, gun.proximityRadius, gun.proximityCollisionMask, QueryTriggerInteraction.Ignore))
        {
            ShootHitPoint(Hit);
        }
        else
        {
            ShootRigidbody(spawnPos);
        }
    }

    public virtual void ShootRigidbody(Transform spawnPos)
    {
        Transform bullet = MainManager.Pooling.TakeBullet();
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos.position + spawnPos.forward * gun.bulletSpawnDistance, spawnPos.rotation, gun);
    }

    public virtual void ShootHitPoint(RaycastHit Hit)
    {
        Hit.collider.GetComponent<Object_Base>()?.Damage(gun.damage, Hit.point, Hit.normal);
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

    public virtual void Reload()
    {
        if (MainManager.Player.ammo[gun] == 0)
            return;

        StartCoroutine(WaitReload());
    }

    public virtual void PlaySoundEffect(AudioClip clip)
    {
        gunAudio.pitch = Random.Range(0.97f, 1.03f);
        gunAudio.PlayOneShot(clip);
    }
}