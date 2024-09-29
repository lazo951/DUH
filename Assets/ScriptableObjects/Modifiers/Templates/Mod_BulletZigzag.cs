using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

[CreateAssetMenu(fileName = "BulletZigzag", menuName = "Scriptable Objects/Modifiers/BulletZigzag")]
public class Mod_BulletZigzag : Mod_Base
{
    public float zigzagInterval;
    private float timer;
    public float xForce;
    public float fwdForce;

    public override void ModifyWeaponDuring(Transform callObject)
    {
        timer += Time.fixedDeltaTime;

        if(timer > zigzagInterval) 
        {
            timer = 0;
            xForce = -xForce;

            Vector3 zig = callObject.right * xForce + callObject.forward * fwdForce;
            callObject.GetComponent<Rigidbody>().velocity = zig;
        }

    }
}
