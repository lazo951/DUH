using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Static : Object_Base
{
    public override void Damage(float dmg, Vector3 impactPoint, Vector3 faceNormal, float projectileScale)
    {
        MainManager.Pooling.PlaceImpact(impactPoint, faceNormal, new Vector3(projectileScale, projectileScale, projectileScale));
    }
}
