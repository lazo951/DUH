using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shotgun : Gun_Base
{
    public int numberOfPellets;
    public float spread;

    public override void ShootRigidbody(Transform spawnPos)
    {
        RaycastHit Hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hit, proximityRadius, interactLayers, QueryTriggerInteraction.Ignore))
        {
            Hit.collider.GetComponent<Object_Base>()?.Damage(gun.damage, Hit.point, Hit.normal);
        }
        else
        {
            for(int i = 0; i < numberOfPellets; i++)
            {
                Vector3 spreadDirection = spawnPos.rotation.eulerAngles;
                spreadDirection.x += Random.Range(-1f, 1f) * spread;
                spreadDirection.y += Random.Range(-1f, 1f) * spread;
                Quaternion rot = Quaternion.Euler(spreadDirection);

                Transform bullet = MainManager.Pooling.TakeBullet();
                bullet?.GetComponent<Bullet>().StartBullet(spawnPos.position, rot, gun);
            }
        }
    }
}
