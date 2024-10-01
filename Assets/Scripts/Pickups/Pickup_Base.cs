using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Base : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(0, 60 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            PickedUp();
    }

    public virtual void PickedUp()
    {
        //
    }

    public virtual void DisplayPickup(string pickupName)
    {
        MainManager.Effects.UIDisplayPickup(pickupName);
    }
}
