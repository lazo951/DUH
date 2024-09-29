using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletExplosive", menuName = "Scriptable Objects/Modifiers/BulletExplosive")]
public class Mod_BulletExplosive : Mod_Base
{
    public float damageIncrease;
    public float explosiveRadius;

    public override void InitialModifyWeapon()
    {
        modForGun.damage += damageIncrease;
    }

    public override void ModifyWeaponColission(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, explosiveRadius);
        foreach (Collider hit in colliders)
        {
            hit.GetComponent<Object_Base>()?.Damage(modForGun.damage, point, Vector3.zero);
            MainManager.Pooling.PlaceExplosion(point);

            //Rigidbody rb = hit.GetComponent<Rigidbody>();

            //if (rb != null)
            //    rb.AddExplosionForce(200, callObject.position, explosiveRadius, 3.0F);
        }
    }
}
