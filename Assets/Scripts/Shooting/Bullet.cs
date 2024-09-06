using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] LayerMask predictLayer;
    GunTemplate firedFromGun;

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        StopAllCoroutines();

        MainManager.Pooling.ReturnBullet(transform);
    }

    private void FixedUpdate()
    {
        PredictCollision();
    }

    public void StartBullet(Vector3 spawnPos, Quaternion spawnRot, GunTemplate gun)
    {
        firedFromGun = gun;
        transform.position = spawnPos;
        transform.rotation = spawnRot;
        transform.localScale = new Vector3(gun.size, gun.size, gun.size);

        gameObject.SetActive(true);
        rb.mass = gun.mass;
        rb.drag = gun.drag;
        rb.isKinematic = false;
        rb.AddForce(transform.forward * gun.speed, ForceMode.Impulse);

        StartCoroutine(DurationEnd(gun.duration));
    }

    private IEnumerator DurationEnd(float dur)
    {
        yield return new WaitForSeconds(dur);
        gameObject.SetActive(false);
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
        RealCollision(collision.gameObject, Vector3.zero);
    }

    private void RealCollision(GameObject hitObject, Vector3 normal)
    {
        ////Debugging
        //Debug.Log("Hit object " + hitObject.name);

        ////Code
        //if(hitObject.GetComponent<Object_Base>())
        //    hitObject.GetComponent<Object_Base>()?.Damage(firedFromGun.damage, transform.position, normal);
        //else
        //    MainManager.Pooling.PlaceDecal(transform.position, normal);

        hitObject.GetComponent<Object_Base>()?.Damage(firedFromGun.damage, transform.position, normal);
        gameObject.SetActive(false);
    }
}
