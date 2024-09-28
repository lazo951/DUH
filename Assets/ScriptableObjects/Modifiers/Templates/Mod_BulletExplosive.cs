using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletExplosive", menuName = "Scriptable Objects/Modifiers/BulletExplosive")]
public class Mod_BulletExplosive : Mod_Base
{
    public override void Modify()
    {
        Debug.Log("BOOM");
    }
}
