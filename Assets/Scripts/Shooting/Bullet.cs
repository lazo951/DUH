using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet;
    [SerializeField] LayerMask predictLayer;

    GunTemplate firedFromGun;
    Rigidbody rb;
    TrailRenderer trail;
    int bounceCounter;

    public void SetupValues()
    {
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    public void StartBullet(Vector3 spawnPos, Quaternion spawnRot, GunTemplate gun, int bCounter)
    {
        //SETUP
        firedFromGun = gun;
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        transform.localScale = new Vector3(gun.size, gun.size, gun.size);
        bounceCounter = bCounter;

        //TRAIL
        trail.colorGradient = gun.bulletTrailColor;
        trail.time = gun.bulletTrailDuration;
        trail.startWidth = gun.size;

        //FORCE
        gameObject.SetActive(true);
        rb.isKinematic = false;
        float finalSpeed = gun.speed / (bounceCounter + 1);
        rb.AddForce(transform.forward * finalSpeed, ForceMode.Impulse);

        StartCoroutine(DurationEnd(gun.duration));
    }

    private IEnumerator DurationEnd(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.SetActive(false);
    }

    private void FixedUpdate()
    {
        foreach (Mod_Base mod in firedFromGun.ModifiersFixedUpdate)
        {
            mod.ModifyWeaponFixedUpdate(transform);
        }

        rb.AddForce(firedFromGun.forceOverLifetime);
        PredictCollision();
    }

    private void PredictCollision()
    {
        Vector3 prediction = transform.position + rb.velocity * Time.fixedDeltaTime;
        RaycastHit hit;

        if(Physics.Linecast(transform.position, prediction, out hit, predictLayer))
        {
            rb.velocity = Vector3.zero;
            rb.isKinematic = true;
            transform.position = hit.point;
            RealCollision(hit.collider.gameObject, hit.normal);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        RealCollision(collision.gameObject, collision.GetContact(0).normal);
    }

    private void RealCollision(GameObject hitObject, Vector3 normal)
    {
        bounceCounter++;

        foreach(Mod_Base mod in firedFromGun.ModifiersColission)
        {
            mod.ModifyWeaponColission(hitObject, normal, transform.position, firedFromGun, bounceCounter);
        }

        hitObject.GetComponent<Object_Base>()?.Damage(firedFromGun.damage, transform.position, normal, firedFromGun.size);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        StopAllCoroutines();
        MainManager.Pooling.ReturnBullet(transform, isPlayerBullet);
    }
}
