using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class HuntState : State
{
    public NavMeshAgent mike;
    public Animator mikeAnim;
    public GameObject flock;
    public HuntState huntState;

    public WanderState wanderState;

    public override State RunCurrentState()
    {
        if (Input.GetKey("y"))
        {
            return wanderState;
        }
        else
        {
            Transform closestRobo = GetClosestRobo(mike.transform);

            MoveInRobosDirection(closestRobo);
            return this;
        }
    }
    public void MoveInRobosDirection(Transform nearest)
    {
        mikeAnim.SetBool("Seek", false);
        mikeAnim.SetBool("Hunt", true);
        mike.speed = 8f;

        mike.SetDestination(nearest.position);
    }

    //adapted source https://answers.unity.com/questions/1236558/finding-nearest-game-object.html

    Transform GetClosestRobo(Transform fromThis)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = fromThis.position;
        foreach (Transform potentialTarget in flock.transform)
        {
            if (potentialTarget.gameObject.tag == "Robo")
            {

                Vector3 directionToTarget = potentialTarget.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    bestTarget = potentialTarget;
                }
            }
        }
        return bestTarget;
    }
}
