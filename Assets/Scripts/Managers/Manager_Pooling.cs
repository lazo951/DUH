using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Manager_Pooling : MonoBehaviour
{
    [Header("Player Bullets")]
    [SerializeField] List<Transform> playerBulletPool = new List<Transform>();
    [SerializeField] GameObject playerBulletPrefab;
    [SerializeField] int playerBulletPoolSize;
    [SerializeField] Transform bulletParent;

    [Header("Enemy Bullets")]
    [SerializeField] List<Transform> enemyBulletPool = new List<Transform>();
    [SerializeField] GameObject enemyBulletPrefab;
    [SerializeField] int enemyBulletPoolSize;

    [Header("Impacts")]
    [SerializeField] List<Transform> impactEffectPool = new List<Transform>();
    [SerializeField] GameObject impactPrefab;
    [SerializeField] int impactPoolSize;
    [SerializeField] Transform impactParent;
    int impactPoolCounter;

    [Header("Explosions")]
    [SerializeField] List<Transform> explosionPool = new List<Transform>();
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] int explosionPoolSize;
    int explosionPoolCounter;

    [Header("Enemies")]
    [SerializeField] List<EnemyTemplate> allEnemies = new List<EnemyTemplate>();
    Dictionary<EnemyTemplate, List<Transform>> enemies = new Dictionary<EnemyTemplate, List<Transform>>();
    [SerializeField] int enemyPoolSize;
    [SerializeField] Transform enemyParent;

    [Header("Enemy Particle Effects")]
    [SerializeField] GameObject enemySpawnPrefab;
    [SerializeField] GameObject enemyDiePrefab;
    [SerializeField] GameObject enemyDropPrefab;
    Dictionary<enemyParticleType, List<Transform>> enemyParticles = new Dictionary<enemyParticleType, List<Transform>>();
    [SerializeField] int particlePoolSize;
    [SerializeField] Transform particleParent;
    int particleSpawnCounter, particleDieCounter, particleDropCounter;

    public void SetupValues()
    {
        SpawnBullets();
        SpawnImpacts();
        SpawnEnemies();
        SpawnEnemyParticles();
    }

    #region Spawn

    private void SpawnBullets()
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
    }

    private void SpawnImpacts()
    {
        for (int i = 0; i < impactPoolSize; i++)
        {
            impactEffectPool.Add(Instantiate(impactPrefab, impactParent).transform);
            impactEffectPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < explosionPoolSize; i++)
        {
            explosionPool.Add(Instantiate(explosionPrefab, impactParent).transform);
            //explosionPool[i].gameObject.SetActive(false);
        }
    }

    private void SpawnEnemies()
    {
        foreach (EnemyTemplate enem in allEnemies)
        {
            List<Transform> tempList = new List<Transform>();
            for (int i = 0; i < enemyPoolSize; i++)
            {
                tempList.Add(Instantiate(enem.enemyPrefab, enemyParent).transform);
                tempList[i].GetComponent<AIThink_Base>().SetupValues();
            }

            enemies.Add(enem, tempList);
            foreach (Transform enemObject in enemies[enem])
            {
                enemObject.gameObject.SetActive(false);
            }
        }
    }

    private void SpawnEnemyParticles()
    {
        List<Transform> tempListSpawn = new List<Transform>();
        List<Transform> tempListDie = new List<Transform>();
        List<Transform> tempListDrop = new List<Transform>();
        for (int i = 0; i < particlePoolSize; i++)
        {
            tempListSpawn.Add(Instantiate(enemySpawnPrefab, particleParent).transform);
            tempListDie.Add(Instantiate(enemyDiePrefab, particleParent).transform);
            tempListDrop.Add(Instantiate(enemyDropPrefab, particleParent).transform);
            //tempListSpawn[i].gameObject.SetActive(false);
            //tempListDie[i].gameObject.SetActive(false);
            //tempListDrop[i].gameObject.SetActive(false);
        }

        enemyParticles.Add(enemyParticleType.spawn, tempListSpawn);
        enemyParticles.Add(enemyParticleType.die, tempListDie);
        enemyParticles.Add(enemyParticleType.drop, tempListDrop);
    }

    #endregion

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

    #region Impacts

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
        //explosionPool[explosionPoolCounter].gameObject.SetActive(true);
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

    #region Enemy Particles

    public void PlaceParticle(enemyParticleType type, Vector3 pos)
    {
        if (type == enemyParticleType.spawn && enemyParticles.ContainsKey(enemyParticleType.spawn))
            PlaceSpawn(pos);
        else if(type == enemyParticleType.die && enemyParticles.ContainsKey(enemyParticleType.die))
            PlaceDie(pos);
        else if (type == enemyParticleType.drop && enemyParticles.ContainsKey(enemyParticleType.drop))
            PlaceDrop(pos);
    }

    private void PlaceSpawn(Vector3 pos)
    {
        enemyParticles[enemyParticleType.spawn][particleSpawnCounter].position = pos;
        enemyParticles[enemyParticleType.spawn][particleSpawnCounter].GetComponent<ParticleSystem>().Play();

        particleSpawnCounter++;
        if (particleSpawnCounter >= particlePoolSize)
            particleSpawnCounter = 0;
    }

    private void PlaceDie(Vector3 pos)
    {
        enemyParticles[enemyParticleType.die][particleDieCounter].position = pos;
        enemyParticles[enemyParticleType.die][particleDieCounter].GetComponent<ParticleSystem>().Play();

        particleDieCounter++;
        if (particleDieCounter >= particlePoolSize)
            particleDieCounter = 0;
    }

    private void PlaceDrop(Vector3 pos)
    {
        enemyParticles[enemyParticleType.drop][particleDropCounter].position = pos;
        enemyParticles[enemyParticleType.drop][particleDropCounter].GetComponent<ParticleSystem>().Play();

        particleDropCounter++;
        if (particleDropCounter >= particlePoolSize)
            particleDropCounter = 0;
    }

    #endregion
}

public enum enemyParticleType
{
    spawn,
    die,
    drop
}