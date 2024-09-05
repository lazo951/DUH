using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Pooling : MonoBehaviour
{
    [SerializeField] List<Transform> bulletPool = new List<Transform>();

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] int bulletPoolSize;
    [SerializeField] Transform bulletParent;

    //public LayerMask playerBulletCollisionMask;
    //public LayerMask enemyBulletCollisionMask;

    [SerializeField] List<Transform> decalPool = new List<Transform>();
    [SerializeField] GameObject decalPrefab;
    [SerializeField] int decalPoolSize;
    [SerializeField] Transform decalParent;
    int decalPoolCounter;

    public void SetupValues()
    {
        for (int i = 0; i < bulletPoolSize; i++)
        {
            bulletPool.Add(Instantiate(bulletPrefab, bulletParent).transform);
            bulletPool[i].gameObject.SetActive(false);
        }

        for(int i  = 0; i < decalPoolSize; i++) 
        {
            decalPool.Add(Instantiate(decalPrefab, decalParent).transform);
            decalPool[i].gameObject.SetActive(false);
        }
    }

    public Transform TakeBullet()
    {
        if (bulletPool.Count > 0)
        {
            Transform bullet = bulletPool[0];
            bulletPool.RemoveAt(0);
            return bullet;
        }

        return null;
    }

    public void ReturnBullet(Transform bullet)
    {
        if (!bulletPool.Contains(bullet))
        {
            bulletPool.Add(bullet);
        }
    }

    public void PlaceDecal(Vector3 pos, Vector3 normal)
    {
        if (normal == Vector3.zero)
            return;

        decalPool[decalPoolCounter].gameObject.SetActive(true);
        decalPool[decalPoolCounter].position = pos;
        decalPool[decalPoolCounter].rotation = Quaternion.LookRotation(normal);

        decalPoolCounter++;
        if (decalPoolCounter >= decalPoolSize)
            decalPoolCounter = 0;
    }

    public void ResetDecals()
    {
        decalPoolCounter = 0;

        foreach(Transform dec in decalPool)
            dec.gameObject.SetActive(false);
    }
}