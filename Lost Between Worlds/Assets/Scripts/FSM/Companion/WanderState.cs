using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class WanderState : State
{
    public NavMeshAgent mike;

    public Animator mikeAnim;
    public Transform player;
    public SeekState seekState;

    public IdleState idleState;

    public bool isEnemyInRange;
    public bool isPlayerInRange;
    private Vector3 lastPlayerPosition;

    public override State RunCurrentState()
    {
        if (Input.GetKey("y"))
        {
            return seekState;
        }
        else
        {
            if (PlayerInRange())
            {
                isPlayerInRange = true;
                return idleState;
            }

            else
            {
                isPlayerInRange = false;
                return this;
            }
        }

    }

    public void Update()
    {
        if (isPlayerInRange)
        {
            mikeAnim.SetBool("Dance", true);
        }
        else
        {
            WalkToPlayer();
        }

    }

    public void WalkToPlayer()
    {
        mikeAnim.SetBool("Dance", false);
        mikeAnim.SetBool("Seek", false);
        mikeAnim.SetBool("Hunt", false);

        if (lastPlayerPosition != player.position && !(PlayerInRange()))
        {
            lastPlayerPosition = player.position;
            mike.SetDestination(player.position);
            mike.transform.LookAt(player.position);
            mikeAnim.SetBool("Wander", true);
        }


    }

    public bool PlayerInRange()
    {
        return (Vector3.Distance(player.position, mike.transform.position) <= 2.0f);
    }
}
