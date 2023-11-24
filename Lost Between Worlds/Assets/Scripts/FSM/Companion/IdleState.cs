using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class IdleState : State
{
    public NavMeshAgent mike;

    public Animator mikeAnim;
    public Transform player;
    public SeekState seekState;
    public WanderState wanderState;

    public override State RunCurrentState()
    {
        if (Input.GetKey("y"))
        {
            return seekState;
        }
        else
        {
            if (!PlayerInRange())
            {
                return wanderState;
            }
            else
            {
                mikeAnim.SetBool("Dance", false);
                mikeAnim.SetBool("Wander", false);
                return this;
            }
        }
    }

    public bool PlayerInRange()
    {
        return (Vector3.Distance(player.position, mike.transform.position) <= 2.0f);
    }
}
