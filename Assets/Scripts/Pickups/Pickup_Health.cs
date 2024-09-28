using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : Pickup_Base
{
    [SerializeField] float healAmount;

    public override void PickedUp()
    {
        if (MainManager.Player.health < MainManager.Player.maxHealth)
        {
            MainManager.Player.ChangeHealth(healAmount);
            DisplayPickup();
            Destroy(gameObject);
        }
    }
}
