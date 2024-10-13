using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_EnemyBodyPart : Object_Base
{
    AIThink_Base scriptMain;
    public float multiplier;

    private void Start()
    {
        scriptMain = GetComponentInParent<AIThink_Base>();
    }

    public override void Damage(float dmg, Vector3 impactPoint, Vector3 faceNormal, float projectileScale, bool isPlayer)
    {
        scriptMain.Damage(dmg * multiplier, impactPoint, faceNormal, isPlayer);
    }
}
