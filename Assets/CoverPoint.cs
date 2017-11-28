using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Troschuetz.Random;

public class CoverPoint : MonoBehaviour {
    GameObject turret;
    public int iterations;
    public float vicinityDist;
    NormalDistribution nDist;
    public bool drawGizmos;
    public bool covered;
    GameObject cover;
    CapsuleCollider player;
    List<Detection> lights;
    List<GameObject> coverPoints;
    AgentScript agent;
    List<float> rand;
    int mask;
    // Use this for initialization
    void Start () {
        turret = GameObject.FindWithTag("Turret");
        mask = 1 << 8;
        mask = ~mask;
        drawGizmos = true;
        covered = false;
        rand = new List<float>();
        agent = GetComponent<AgentScript>();
        player = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider>();
        nDist = new NormalDistribution();
        coverPoints = new List<GameObject>();
        lights = new List<Detection>(GameObject.Find("Spot Lights").GetComponentsInChildren<Detection>());
        foreach (Transform child in GameObject.Find("Cover points").transform)
        {
            coverPoints.Add(child.gameObject);
        }
	}

    void Update()
    {
        foreach (Detection d in lights)
        {
            if (d.detected && !covered)
            {
                Debug.Log("Detected");
                FindCover();
            }
        }
    }
	
	void OnDrawGizmosSelected()
    {
        if (drawGizmos && covered)
        {
            Gizmos.color = Color.red;
            int i;
            Vector3 dir;
            nDist.Reset();
            if(rand.Count == 0)
            {
                for (i = 0; i < iterations; i++)
                {
                    rand.Add((float)nDist.NextDouble());
                    rand.Add(Random.value);
                    rand.Add((float)nDist.NextDouble());
                }
            }
            for (i = 0; i < iterations; i++)
            {
                dir = cover.transform.position;
                dir.x += rand[i*3] /3f * player.radius;
                dir.y += rand[i*3+1] * player.height * player.transform.localScale.y;
                dir.z += rand[i*3+2] /3f * player.radius;
                dir = (dir - turret.transform.position).normalized;
                Gizmos.DrawRay(turret.transform.position, dir*150);
            }
        }
    }

    void FindCover()
    {
        float coverQuality = 0;
        foreach(GameObject c in coverPoints)
        {
            if ((c.transform.position - player.transform.position).magnitude <= vicinityDist)
            {
                float tmp = getCoverQuality(c);
                Debug.Log(c.name + " is " + (c.transform.position - player.transform.position).magnitude + " away and quality is " + tmp);
                if (cover == null)
                {
                    coverQuality = tmp;
                    cover = c;
                }
                else if (tmp == coverQuality && (c.transform.position - player.transform.position).magnitude < (cover.transform.position - player.transform.position).magnitude || tmp > coverQuality)
                {
                    coverQuality = tmp;
                    cover = c;
                }
            }
        }
        if(cover!= null)
            agent.SetAgent(cover);
        covered = true;
    }

    float getCoverQuality(GameObject cover)
    {
        Vector3 from, to;
        int hits = 0;
        int i;
        RaycastHit hit;
        Vector3 tmpLoc = player.transform.position;
        player.transform.position = cover.transform.position;
        player.transform.Translate(new Vector3(0, tmpLoc.y+0.3231387f, 0));
        nDist.Reset();
        for (i = 0; i < iterations; i++)
        {
            from = turret.transform.position;
            to = cover.transform.position;
            to.x += (Mathf.Min(3,(float)nDist.NextDouble()) / 3f) * player.radius;
            to.y += Random.value*player.height*player.transform.localScale.y;
            to.z += (Mathf.Min(3,(float)nDist.NextDouble()) / 3f) * player.radius;
            Vector3 dir = to - from;
            dir.Scale(new Vector3(2f, 2f, 2f));
            if (Physics.Raycast(from, dir, out hit, 150f,mask))
            {
                if (hit.collider.tag == "Player")
                {
                    hits++;
                }
            }
        }
        player.transform.position = tmpLoc;
        return 1 - (float)hits / iterations;
    }

}
