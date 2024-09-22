using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Base : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            PickedUp();
    }

    public virtual void PickedUp()
    {
        //
    }
}
