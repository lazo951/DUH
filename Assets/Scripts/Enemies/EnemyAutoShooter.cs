using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAutoShooter : MonoBehaviour
{
    public GunTemplate gun;
    bool bulletInChamber = true;

    void Start()
    {
        //
    }

    void Update()
    {
        Shoot(transform);
    }

    private void Shoot(Transform spawnPos)
    {
        if (!bulletInChamber)
            return;

        bulletInChamber = false;

        Transform bullet = MainManager.Pooling.TakeEnemyBullet();
        bullet?.GetComponent<Bullet>().StartBullet(spawnPos.position + spawnPos.forward * gun.bulletSpawnDistance, spawnPos.rotation, gun);

        StartCoroutine(FireRate());
    }

    private IEnumerator FireRate()
    {
        yield return new WaitForSeconds(60f/gun.rateOfFireRPM);
        bulletInChamber = true;
    }
}
