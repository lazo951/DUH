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

    bool isMoving;
    Vector3 correctedDestination, finalDestination;
    Quaternion finalRotation;

    public override void SetupValues(float moveSpeed, float turnSpeed)
    {
        rb = GetComponent<Rigidbody>();
        moveForce = moveSpeed;
        turnForce = turnSpeed;

        finalRotation = Quaternion.identity;
    }

    public override void MoveTo(Vector3 destination)
    {
        isMoving = false;
        correctedDestination = destination;
        correctedDestination += Random.insideUnitSphere * flyRandomness;

        RaycastHit hit;
        if (Physics.Raycast(correctedDestination, Vector3.down, out hit, 100, colideLayer, QueryTriggerInteraction.Ignore))
        {
            correctedDestination = hit.point + Vector3.up * flyHeight;
        }

        if (Physics.Linecast(transform.position, correctedDestination, colideLayer))
        {
            MoveTo(correctedDestination);
            return;
        }

        LookAt(correctedDestination);
        isMoving = true;
    }

    private void FixedUpdate()
    {
        if(isMoving)
        {
            finalDestination = Vector3.MoveTowards(transform.position, correctedDestination, moveForce * Time.deltaTime);
            rb.MovePosition(finalDestination);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, turnForce * Time.deltaTime);
    }

    public override void LookAt(Vector3 position)
    {
        Vector3 direction = position - transform.position;
        finalRotation = Quaternion.LookRotation(direction.normalized);
    }
}
