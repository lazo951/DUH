using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMove_Flying : AIMove_Base
{
    Rigidbody rb;
    float moveForce, turnForce;

    public LayerMask colideLayer;
    public float flyRandomness;
    public float flyHeight;
    public float heightRandomness;
    
    public int maxRecursions;

    Vector3 correctedDestination;
    Quaternion finalRotation;

    public override void SetupValues(float moveSpeed, float turnSpeed)
    {
        rb = GetComponent<Rigidbody>();
        moveForce = moveSpeed;
        turnForce = turnSpeed;

        finalRotation = Quaternion.identity;
    }

    public override void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public override void MoveTo(Vector3 destination, int counter)
    {
        counter++;
        correctedDestination = destination;
        correctedDestination += Random.insideUnitSphere * flyRandomness;

        RaycastHit hit;
        if (Physics.Raycast(correctedDestination, Vector3.down, out hit, 100, colideLayer, QueryTriggerInteraction.Ignore))
        {
            correctedDestination = hit.point + Vector3.up * (flyHeight + Random.Range(0f, heightRandomness));
        }

        if (Physics.Linecast(transform.position, correctedDestination, colideLayer) && counter < maxRecursions)
        {
            MoveTo(correctedDestination, counter);
            return;
        }

        //float dist = Vector3.Distance(transform.position, correctedDestination);
        //if (Physics.SphereCast(transform.position, 1.5f, correctedDestination, out hit, dist, colideLayer) && counter < maxRecursions)
        //{
        //    MoveTo(correctedDestination, counter);
        //    return;
        //}

        LookAt(correctedDestination);
        AddForceTowards(correctedDestination);
    }

    private void AddForceTowards(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        rb.AddForce(direction * moveForce, ForceMode.Force);
    }

    private void FixedUpdate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, turnForce * Time.deltaTime);
    }

    public override void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        finalRotation = Quaternion.LookRotation(direction.normalized);
    }
}
