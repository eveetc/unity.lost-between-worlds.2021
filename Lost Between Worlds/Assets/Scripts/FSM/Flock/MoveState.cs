using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class MoveState : RoboState
{
    public NavMeshAgent robo;

    public Animator roboAnim;
    public Transform mike;
    public FleeState fleeState;

    public override RoboState RunCurrentState()
    {
        if (EnemyInRange())
        {
            return fleeState;
        }

        else
        {
            WalkAround();
            return this;
        }
    }



    public void WalkAround()
    {
        robo.SetDestination(robo.transform.position + new Vector3(0.01f, 0f, 0.01f));
    }

    public bool EnemyInRange()
    {
        return (Vector3.Distance(mike.position, robo.transform.position) <= 8.0f);
    }
}
