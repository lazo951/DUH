using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIMove_NavMesh : AIMove_Base
{
    NavMeshAgent agent;

    [SerializeField] AnimationCurve jumpCurve = new AnimationCurve();
    [SerializeField] float jumpSpeed;

    float turnSpeed;
    Quaternion finalRotation;

    public override void SetupValues(float moveSpeed, float turnSpeed)
    {
        this.turnSpeed = turnSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.angularSpeed = 0;

        finalRotation = Quaternion.identity;
    }

    public override void SetPosition(Vector3 position)
    {
        NavMeshHit correctedPos;

        if (NavMesh.SamplePosition(position, out correctedPos, 3, NavMesh.AllAreas))
            agent.Warp(correctedPos.position);
    }

    public override void MoveTo(Vector3 destination, int counter)
    {
        agent.isStopped = false;
        NavMeshHit correctedPos;

        if (NavMesh.SamplePosition(destination, out correctedPos, 3, NavMesh.AllAreas))
        {
            NavMeshPath newPath = new NavMeshPath();
            NavMesh.CalculatePath(transform.position, correctedPos.position, NavMesh.AllAreas, newPath);
            if(newPath.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetPath(newPath);
                LookAt(agent.steeringTarget);
                return;
            }
            else
            {
                agent.isStopped = true;
            }
        }
        else
        {
            agent.isStopped = true;
        }


        LookAt(correctedPos.position);
    }

    public override void Stop()
    {
        agent.isStopped = true;
    }

    private void Update()
    {
        if (agent.isOnOffMeshLink)
        {
            StartCoroutine(jumpWait(jumpSpeed));
            agent.CompleteOffMeshLink();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, finalRotation, turnSpeed * Time.deltaTime);
    }

    private IEnumerator jumpWait(float duration)
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = agent.currentOffMeshLinkData.endPos + Vector3.up * agent.baseOffset;
        float timer = 0f;

        while (timer < 1f)
        {
            float yOffset = jumpCurve.Evaluate(timer);
            agent.transform.position = Vector3.Lerp(startPos, endPos, timer) + yOffset * Vector3.up;
            timer += Time.deltaTime / duration;
            yield return null;
        }
    }

    public override void LookAt(Vector3 position)
    {
        Vector3 tempPos = position;
        tempPos.y = transform.position.y;
        Vector3 direction = tempPos - transform.position;

        if(direction != Vector3.zero)
            finalRotation = Quaternion.LookRotation(direction.normalized);
    }
}
