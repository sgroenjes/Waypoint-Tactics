using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour {
    MeshRenderer mesh;
    public bool detected;
	// Use this for initialization
	void Start () {
        detected = false;
        mesh = GetComponentInChildren<MeshRenderer>();
	}

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            detected = false;
            GameObject.FindWithTag("Player").GetComponent<AgentScript>().ContinuePath();
            GameObject.FindWithTag("Player").GetComponent<CoverPoint>().covered = false;
        }
    }
	
	void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
            mesh.material.SetColor("_EmissionColor", Color.red);
        }
    }
}
