using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLocomotion : MonoBehaviour
{
    [HideInInspector]
    public NavMeshAgent agent;
    
    public bool crouch = false;


    void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
    }
	

    /// <summary>
    /// Move the enemy to a certain position
    /// </summary>
    /// <param name="destination">The position the enemy will go</param>
    public void MoveTo(BallCover destination)
    {
        agent.destination = destination.transform.position;
        transform.LookAt(destination.transform.position);
    }

    /// <summary>
    /// Make the enemy to crouch
    /// </summary>
    public void Crouch()
    {
        crouch = true;
        transform.localScale = new Vector3(1f, 0.5f, 1f);
    }

    /// <summary>
    /// Make the enemy to stand up
    /// </summary>
    public void ResetCrouch()
    {
        crouch = false;
        transform.localScale = new Vector3(1f, 1f, 1f);
    }



    // ATM not used:

    /// <summary>
    /// Check if the enemy has reach the destination
    /// </summary>
    /// <returns>Returns whether or not the destination has been reached</returns>
    public bool CheckReachDestination()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    // Attack? 
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Like Mathf.Approximately but being able to modify the epsilon
    /// </summary>
    /// <param name="a">Firts parameter</param>
    /// <param name="b">Second parameter</param>
    /// <param name="epsilon">Epsilon</param>
    /// <returns>Return true if the two parameters are similars</returns>
    private bool isNear(float a, float b, float epsilon)
    {
        return (Mathf.Abs(a - b) < epsilon);
    }
}
