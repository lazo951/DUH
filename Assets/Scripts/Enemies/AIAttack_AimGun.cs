using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttack_AimGun : AIAttack_Base
{
    public override void AimAt(Transform target)
    {
        gunMuzzle.LookAt(target);
        base.AimAt(target);
    }
}
