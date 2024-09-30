using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun_Base
{
    public int numberOfPellets;
    public float spread;

    public override void CheckProximity(Vector3 spawnPos, Transform spawnSource)
    {
        for (int i = 0; i < numberOfPellets; i++)
        {
            Vector3 spreadDirection = spawnSource.forward + spawnSource.up*Random.Range(-spread, spread) + spawnSource.right* Random.Range(-spread, spread);
            RaycastHit Hit;

            if (Physics.Raycast(spawnPos, spreadDirection, out Hit, gun.proximityRadius, gun.proximityCollisionMask, QueryTriggerInteraction.Ignore))
            {
                ShootHitPoint(Hit);
                //Debug.DrawRay(spawnPos, spreadDirection, Color.red, 10f);
            }
            else
            {
                ShootRigidbody(spawnPos, spawnSource, spreadDirection);
                //Debug.DrawRay(spawnPos, spreadDirection, Color.green, 10f);
            }
        }
    }

    public void ShootRigidbody(Vector3 spawnPos, Transform spawnSource, Vector3 spawnDirection)
    {
        Quaternion rot = Quaternion.LookRotation(spawnDirection);

        Transform bullet = MainManager.Pooling.TakePlayerBullet();
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos + spawnSource.forward * gun.bulletSpawnDistance, rot, gun);
    }
}
