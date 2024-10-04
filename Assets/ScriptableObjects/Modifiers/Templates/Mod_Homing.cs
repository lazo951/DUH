using UnityEngine;

[CreateAssetMenu(fileName = "BulletHoming", menuName = "Scriptable Objects/Modifiers/BulletHoming")]
public class Mod_Homing : Mod_Base
{
    public LayerMask enemyLayer;
    public float homingRadius;
    public float fwdForce;
    Vector3 dir;

    public override void ModifyWeaponFixedUpdate(Transform callObject)
    {
        Collider[] colliders = Physics.OverlapSphere(callObject.position, homingRadius, enemyLayer);
        if(colliders.Length > 0 ) 
        {
            dir = colliders[Random.Range(0,colliders.Length)].transform.position - callObject.position;
            callObject.GetComponent<Rigidbody>().velocity = dir.normalized * fwdForce;
        }
    }
}
