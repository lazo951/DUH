using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Base : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PickedUp(collision.gameObject.transform);
        }
    }

    public virtual void PickedUp(Transform player)
    {
        //
    }
}
