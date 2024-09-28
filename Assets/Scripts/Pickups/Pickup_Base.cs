using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Base : MonoBehaviour
{
    public string pickupName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            PickedUp();
    }

    public virtual void PickedUp()
    {
        //
    }

    public virtual void DisplayPickup()
    {
        MainManager.Effects.UIDisplayPickup(pickupName);
    }
}
