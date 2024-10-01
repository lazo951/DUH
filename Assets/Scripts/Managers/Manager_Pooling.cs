using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Manager_Pooling : MonoBehaviour
{
    //Player bullets
    [SerializeField] List<Transform> playerBulletPool = new List<Transform>();
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] int playerBulletPoolSize;
    [SerializeField] Transform bulletParent;

    //Enemy bullets
    [SerializeField] List<Transform> enemyBulletPool = new List<Transform>();
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] int enemyBulletPoolSize;

    //Impact effects
    [SerializeField] List<Transform> impactEffectPool = new List<Transform>();
    [SerializeField] GameObject impactPrefab;
    [SerializeField] int impactPoolSize;
    [SerializeField] Transform impactParent;
    int impactPoolCounter;

    //Explosion effects
    [SerializeField] List<Transform> explosionPool = new List<Transform>();
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] int explosionPoolSize;
    int explosionPoolCounter;

    public void SetupValues()
    {
        for (int i = 0; i < playerBulletPoolSize; i++)
        {
            playerBulletPool.Add(Instantiate(playerBulletPrefab, bulletParent).transform);
            playerBulletPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            enemyBulletPool.Add(Instantiate(enemyBulletPrefab, bulletParent).transform);
            enemyBulletPool[i].gameObject.SetActive(false);
        }

        for (int i  = 0; i < impactPoolSize; i++) 
        {
            impactEffectPool.Add(Instantiate(impactPrefab, impactParent).transform);
            impactEffectPool[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < explosionPoolSize; i++)
        {
            explosionPool.Add(Instantiate(explosionPrefab, impactParent).transform);
            explosionPool[i].gameObject.SetActive(false);
        }
    }

    public Transform TakePlayerBullet()
    {
        if (playerBulletPool.Count > 0)
        {
            Transform bullet = playerBulletPool[0];
            playerBulletPool.RemoveAt(0);
            return bullet;
        }

        return null;
    }

    public Transform TakeEnemyBullet()
    {
        if (enemyBulletPool.Count > 0)
        {
            Transform bullet = enemyBulletPool[0];
            enemyBulletPool.RemoveAt(0);
            return bullet;
        }

        return null;
    }

    public void ReturnPlayerBullet(Transform bullet)
    {
        if (!playerBulletPool.Contains(bullet))
        {
            playerBulletPool.Add(bullet);
        }
    }

    public void ReturnEnemyBullet(Transform bullet)
    {
        if (!enemyBulletPool.Contains(bullet))
        {
            enemyBulletPool.Add(bullet);
        }
    }

    public void PlaceImpact(Vector3 pos, Vector3 normal, Vector3 scale)
    {
        if (normal == Vector3.zero)
            return;

        impactEffectPool[impactPoolCounter].gameObject.SetActive(true);
        impactEffectPool[impactPoolCounter].position = pos;
        impactEffectPool[impactPoolCounter].GetComponent<DecalProjector>().size = scale;
        impactEffectPool[impactPoolCounter].rotation = Quaternion.LookRotation(normal);
        impactEffectPool[impactPoolCounter].GetComponent<Impact>().PlayImpact();

        impactPoolCounter++;
        if (impactPoolCounter >= impactPoolSize)
            impactPoolCounter = 0;
    }

    public void ResetImpacts()
    {
        impactPoolCounter = 0;

        foreach(Transform dec in impactEffectPool)
            dec.gameObject.SetActive(false);
    }

    public void PlaceExplosion(Vector3 pos, Vector3 scale)
    {
        explosionPool[explosionPoolCounter].gameObject.SetActive(true);
        explosionPool[explosionPoolCounter].position = pos;
        explosionPool[explosionPoolCounter].localScale = scale/2;
        explosionPool[explosionPoolCounter].GetComponent<Impact>().PlayImpact();

        explosionPoolCounter++;
        if(explosionPoolCounter >= explosionPoolSize)
            explosionPoolCounter = 0;
    }
}