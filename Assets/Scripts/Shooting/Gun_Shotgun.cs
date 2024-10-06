using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun_Base
{
    public int numberOfPellets;
    public List<Gradient> rainbowColors = new List<Gradient>();

    public override void CheckProximity(Vector3 spawnPos, Transform spawnSource)
    {
        for (int i = 0; i < numberOfPellets; i++)
        {
            Vector3 spreadDirection = spawnSource.forward + spawnSource.up*Random.Range(-gun.spread, gun.spread) + spawnSource.right* Random.Range(-gun.spread, gun.spread);
            RaycastHit Hit;

            if (Physics.Raycast(spawnPos, spreadDirection, out Hit, gun.proximityRadius, gun.proximityCollisionMask, QueryTriggerInteraction.Ignore))
            {
                ShootHitPoint(Hit);
            }
            else
            {
                gun.bulletTrailColor = rainbowColors[Random.Range(0, rainbowColors.Count)];
                ShootRigidbody(spawnPos, spawnSource, spreadDirection);
            }
        }
    }

    public void ShootRigidbody(Vector3 spawnPos, Transform spawnSource, Vector3 spawnDirection)
    {
        Quaternion rot = Quaternion.LookRotation(spawnDirection);

        Transform bullet = MainManager.Pooling.TakeBullet(gun.isPlayerGun);
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos + spawnSource.forward * gun.bulletSpawnDistance, rot, gun, 0);
    }
}
