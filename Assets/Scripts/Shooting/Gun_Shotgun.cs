using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun_Base
{
    public int numberOfPellets;
    public float spread;

    public override void CheckProximity(Transform spawnPos)
    {
        for (int i = 0; i < numberOfPellets; i++)
        {
            Vector3 spreadDirection = spawnPos.forward + spawnPos.up*Random.Range(-spread, spread) + spawnPos.right* Random.Range(-spread, spread);
            //spreadDirection.Normalize();
            RaycastHit Hit;

            if (Physics.Raycast(spawnPos.position, spreadDirection, out Hit, gun.proximityRadius, gun.proximityCollisionMask, QueryTriggerInteraction.Ignore))
            {
                ShootHitPoint(Hit);
                Debug.DrawRay(spawnPos.position, spreadDirection, Color.red, 10f);
            }
            else
            {
                ShootRigidbody(spawnPos, spreadDirection);
                Debug.DrawRay(spawnPos.position, spreadDirection, Color.green, 10f);
            }
        }
    }

    public void ShootRigidbody(Transform spawnPos, Vector3 spawnDirection)
    {
        Quaternion rot = Quaternion.LookRotation(spawnDirection);

        Transform bullet = MainManager.Pooling.TakePlayerBullet();
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos.position + spawnPos.forward * gun.bulletSpawnDistance, rot, gun);
    }
}
