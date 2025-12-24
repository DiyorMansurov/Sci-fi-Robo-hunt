using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private enum _state
    {
        Walking,
        Jumping,
        Attacking,
        Dead,
    }

    [SerializeField]
    private _state currentState = _state.Walking;

    [SerializeField]
    private List<GameObject> Waypoints;
    private int currentWaypointIndex = 0;
    private NavMeshAgent agent;
    private Vector3 destination;
    private bool isReversing = false;
    private bool isAttacking = false;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
        agent.destination = Waypoints[currentWaypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        StateChange();
        AIBehavior();
    }

    private void StateChange()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentState = _state.Jumping;
        }
    }

    private void AIBehavior()
    {
        switch (currentState)
        {
            case _state.Walking:
                MoveToNextWaypoint();
                break;
            case _state.Jumping:
                Debug.Log("Jumping");
                agent.isStopped = true;
                break;
            case _state.Attacking:
                if (!isAttacking)
                {
                    StartCoroutine(AttackDelay());
                }
                break;
            case _state.Dead:
                break;
            default:
                break;
        }
    }

    private void MoveToNextWaypoint()
    {
        if (agent.remainingDistance > 0.5f)
            return;

        if (!isReversing)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= Waypoints.Count)
            {
                currentWaypointIndex = Waypoints.Count - 2;
                isReversing = true;
            }
        } else if (isReversing)
        {
            currentWaypointIndex--;
            if (currentWaypointIndex < 0)
            {
                currentWaypointIndex = 1;
                isReversing = false;
            }
        }

        destination = Waypoints[currentWaypointIndex].transform.position;
        agent.destination = destination;

        currentState = _state.Attacking;
        
    }

    private IEnumerator AttackDelay()
    {
        isAttacking = true;
        Debug.Log("Attacking");
        agent.isStopped = true;
        yield return new WaitForSeconds(3f);
        currentState = _state.Walking;
        agent.isStopped = false;
        isAttacking = false;
    }
}
