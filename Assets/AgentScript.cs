using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentScript : MonoBehaviour {
    public Transform[] waypoints;
    NavMeshAgent agent;
    int currTarget;
	// Use this for initialization
	void Start () {
        currTarget = 0;
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(waypoints[currTarget].position);
    }

    public void NextWaypoint()
    {
        if (currTarget == 6)
            currTarget = 0;
        else
            currTarget++;
        agent.SetDestination(waypoints[currTarget].position);
    }

    public void ContinuePath()
    {
        agent.SetDestination(waypoints[currTarget].position);
    }

    public void SetAgent(GameObject obj)
    {
        agent.SetDestination(obj.transform.position);
    }
    
}
