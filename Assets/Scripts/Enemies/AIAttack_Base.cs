using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack_Base : MonoBehaviour
{
    public GunTemplate gun;
    public Transform gunMuzzle;

    bool bulletInChamber = true;
    AudioSource gunAudio;

    private void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    public virtual void FireGun()
    {
        if (!bulletInChamber)
            return;

        bulletInChamber = false;

        foreach (Mod_Base mod in gun.ModifiersShoot)
        {
            mod.ModifyWeaponShoot(gunMuzzle, transform.gameObject);
        }

        CheckProximity(gunMuzzle.position, gunMuzzle);
        StartCoroutine(FireRate());
    }

    public virtual void CheckProximity(Vector3 spawnPos, Transform spawnSource)
    {
        RaycastHit Hit;

        if (Physics.Raycast(spawnPos, spawnSource.forward, out Hit, gun.proximityRadius, gun.proximityCollisionMask, QueryTriggerInteraction.Ignore))
        {
            ShootHitPoint(Hit);
        }
        else
        {
            ShootRigidbody(spawnPos, spawnSource);
        }
    }

    public virtual void ShootRigidbody(Vector3 spawnPos, Transform spawnSource)
    {
        Transform bullet = MainManager.Pooling.TakeBullet(gun.isPlayerGun);
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos + spawnSource.forward * gun.bulletSpawnDistance, spawnSource.rotation, gun, 0);
    }

    public virtual void ShootHitPoint(RaycastHit Hit)
    {
        Hit.collider.GetComponent<Object_Base>()?.Damage(gun.damage, Hit.point, Hit.normal, gun.size);

        foreach (Mod_Base mod in gun.ModifiersColission)
        {
            mod.ModifyWeaponColission(Hit.collider.gameObject, Hit.normal, Hit.point, gun, 0);
        }
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(60f / gun.rateOfFireRPM);
        bulletInChamber = true;
    }
}
