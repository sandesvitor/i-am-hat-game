using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCFallowsPlayer : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Transform _player;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _navMeshAgent.SetDestination(_player.position);
    }

}
