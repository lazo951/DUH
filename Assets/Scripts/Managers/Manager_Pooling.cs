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

    //Enemies
    [SerializeField] List<EnemyTemplate> allEnemies = new List<EnemyTemplate>();
    Dictionary<EnemyTemplate, List<Transform>> enemies = new Dictionary<EnemyTemplate, List<Transform>>();
    [SerializeField] int enemyPoolSize;
    [SerializeField] Transform enemyParent;

    public void SetupValues()
    {
        for (int i = 0; i < playerBulletPoolSize; i++)
        {
            playerBulletPool.Add(Instantiate(playerBulletPrefab, bulletParent).transform);
            playerBulletPool[i].GetComponent<Bullet>().SetupValues();
            playerBulletPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < enemyBulletPoolSize; i++)
        {
            enemyBulletPool.Add(Instantiate(enemyBulletPrefab, bulletParent).transform);
            enemyBulletPool[i].GetComponent<Bullet>().SetupValues();
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

        foreach(EnemyTemplate enem in allEnemies)
        {
            List<Transform> tempList = new List<Transform>();
            for(int i = 0; i < enemyPoolSize; i++)
            {
                tempList.Add(Instantiate(enem.enemyPrefab, enemyParent).transform);
                tempList[i].GetComponent<AIThink_Base>().SetupValues();
            }

            enemies.Add(enem, tempList);
            foreach(Transform enemObject in enemies[enem])
            {
                enemObject.gameObject.SetActive(false);
            }
        }
    }

    #region Bullets

    public Transform TakeBullet(bool isPlayer)
    {
        if (isPlayer)
            return TakePlayerBullet();
        else
            return TakeEnemyBullet();
    }

    private Transform TakePlayerBullet()
    {
        if (playerBulletPool.Count > 0)
        {
            Transform bullet = playerBulletPool[0];
            playerBulletPool.RemoveAt(0);
            return bullet;
        }

        return null;
    }

    private Transform TakeEnemyBullet()
    {
        if (enemyBulletPool.Count > 0)
        {
            Transform bullet = enemyBulletPool[0];
            enemyBulletPool.RemoveAt(0);
            return bullet;
        }

        return null;
    }

    public void ReturnBullet(Transform bullet, bool isPlayer)
    {
        if (isPlayer)
            ReturnPlayerBullet(bullet);
        else
            ReturnEnemyBullet(bullet);
    }

    private void ReturnPlayerBullet(Transform bullet)
    {
        if (!playerBulletPool.Contains(bullet))
        {
            playerBulletPool.Add(bullet);
        }
    }

    private void ReturnEnemyBullet(Transform bullet)
    {
        if (!enemyBulletPool.Contains(bullet))
        {
            enemyBulletPool.Add(bullet);
        }
    }

    #endregion

    #region Effects

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

    #endregion

    #region Enemies

    public Transform TakeEnemy(EnemyTemplate enem)
    {
        if (enemies[enem].Count > 0)
        {
            Transform newEnemy = enemies[enem][0];
            enemies[enem].RemoveAt(0);
            return newEnemy;
        }

        return null;
    }

    public void ReturnEnemy(EnemyTemplate enem, Transform enemyObject)
    {
        if (!enemies[enem].Contains(enemyObject))
        {
            enemies[enem].Add(enemyObject);
        }
    }

    #endregion
}