using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Pickup_Modifier : Pickup_Base
{
    public Mod_Base modifier;

    public override void PickedUp()
    {
        modifier.AttachModifier();

        DisplayPickup();
        Destroy(gameObject);
    }
}
