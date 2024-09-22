using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Ammo : Pickup_Base
{
    [SerializeField] GunTemplate ammoForGun;
    [SerializeField] int ammoAmount;

    public override void PickedUp()
    {
        if (MainManager.Player.ammo[ammoForGun] < ammoForGun.maxAmmo)
        {
            MainManager.Player.ChangeAmmo(ammoForGun, ammoAmount);
            Destroy(gameObject);
        }
    }
}
