using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Weapon : Pickup_Base
{
    [SerializeField] GunTemplate pickupGun;

    public override void PickedUp()
    {
        MainManager.Shooting.PickupWeapon(pickupGun);
        DisplayPickup();
        Destroy(gameObject);
    }
}
