using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoint : MonoBehaviour {

    public GameObject player;
    AgentScript agent;
    private int hit;
    void Start()
    {
        hit = 1;
        agent = player.GetComponent<AgentScript>();
    }

	void OnTriggerEnter(Collider other)
    {
        hit--;
        if(hit > -1 && !player.GetComponent<CoverPoint>().covered)
        {
            agent.NextWaypoint();
        }
    }

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(HitExit());
    }

    IEnumerator HitExit()
    {
        yield return new WaitForSeconds(1);
        hit = 1;
    }
}

