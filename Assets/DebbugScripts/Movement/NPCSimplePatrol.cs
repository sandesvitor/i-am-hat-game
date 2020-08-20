using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCSimplePatrol : MonoBehaviour
{
    // Dictates wheter the agente waits on each node:
    [SerializeField] private bool _patrolWaiting;

    //The total time we wait at each node:
    [SerializeField] private float _totalWaitTime = 3f;

    //The probability of switching direction:
    [SerializeField] private float _switchProbability = 0.2f;

    //The list of all patrol nodes to visit:
    [SerializeField] private List<Waypoint> _patrolPoints;

    private NavMeshAgent _navMeshAgent;
    private int _currentPatrolIndex;
    private bool _travelling;
    private bool _waiting;
    private bool _patrolForward;
    private float _waitTimer;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (_navMeshAgent == null)
        {
            Debug.LogError("The NavMeshAgent component is not attached to " + this.gameObject);
        }
        else
        {
            if(_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            } 
            else
            {
                Debug.Log("Insufficient patrol points for basic patrolling behaviour.");
            }
        }
    }

    void Update()
    {
        //Check if we're close to the destination:
        if(_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;

            //If we''re going to wait, then wait:
            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;
            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }
        }

        //Instead if we're waiting
        if (_waiting)
        {
            _waitTimer += Time.deltaTime;
            if(_waitTimer >= _totalWaitTime)
            {
                _waiting = false;

                ChangePatrolPoint();
                SetDestination();
            }
        }
    }

    private void SetDestination()
    {
        if(_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }
    }

    //<summary>
    //      Selects a new patrol point in the available list but
    //      also with a small probability allows for us to move forward or backwards.
    //</summary>
    private void ChangePatrolPoint()
    {        
        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;
        }

        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else if (--_currentPatrolIndex < 0)
        {
            _currentPatrolIndex = _patrolPoints.Count - 1;
        }
    }
}
