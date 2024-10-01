using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Ammo : Pickup_Base
{
    [SerializeField] GunTemplate ammoForGun;
    [SerializeField] int ammoAmount;

    public override void PickedUp()
    {
        if (MainManager.Shooting.ammo[ammoForGun] < ammoForGun.maxAmmo)
        {
            MainManager.Shooting.ChangeAmmo(ammoForGun, ammoAmount);
            DisplayPickup(ammoForGun.gunName + " ammo +" + ammoAmount.ToString());
            Destroy(gameObject);
        }
    }
}
