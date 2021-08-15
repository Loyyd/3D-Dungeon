using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : MonoBehaviour
{

    public Transform goal;
    NavMeshAgent agent;

    void Start()
    {
        goal = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Vector3.Distance(transform.position, goal.position) > 1.2)
        {
            agent.destination = goal.position;
        }
        else
        {
            agent.destination = transform.position;
        }
    }
}
