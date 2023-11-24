using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
public class FleeState : RoboState
{

    public NavMeshAgent robo;

    public Animator roboAnim;
    public Transform mike;
    public MoveState moveState;

    public bool isEnemyInRange;
    public override RoboState RunCurrentState()
    {
        if (EnemyInRange())
        {
            isEnemyInRange = true;
            Flee();
            return this;
        }

        else
        {
            isEnemyInRange = false;
            return moveState;
        }
        return this;
    }

    public void Flee()
    {
        Vector3 direction = robo.transform.position - mike.position;
        robo.speed = 7.8f;
        robo.SetDestination(robo.transform.position + direction);
    }

    public bool EnemyInRange()
    {
        return (Vector3.Distance(mike.position, robo.transform.position) <= 8.0f);
    }
}
