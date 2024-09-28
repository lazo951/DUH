using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pickup_Modifier : Pickup_Base
{
    public GunTemplate modifyGun;
    public Mod_Base modifier;

    public override void PickedUp()
    {
        DisplayPickup();
        Destroy(gameObject);
    }
}
