using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class RoboState : MonoBehaviour
{
    public abstract RoboState RunCurrentState();

}
