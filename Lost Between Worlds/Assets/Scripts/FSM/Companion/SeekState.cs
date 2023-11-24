using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class SeekState : State
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
            if (RoboInHuntRange(closestRobo))
            {
                return huntState;
            }
            else
            {
                MoveInRobosDirection(closestRobo);
                return this;
            }
        }
    }

    public void MoveInRobosDirection(Transform nearest)
    {
        mike.speed = 6.5f;
        mikeAnim.SetBool("Seek", true);
        mike.SetDestination(nearest.position);
    }

    //adapted source https://answers.unity.com/questions/1236558/finding-nearest-game-object.html
    Transform GetClosestRobo(Transform fromThis)
    {
        //List<Transform> enemies
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

    public bool RoboInHuntRange(Transform obj)
    {
        return (Vector3.Distance(obj.position, mike.transform.position) <= 10.0f);
    }
}
