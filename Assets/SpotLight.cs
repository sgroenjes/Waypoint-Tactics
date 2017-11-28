using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLight : MonoBehaviour {
    List<Transform> spotLights;
    GameObject spotLightParent;
    float timer;
    float angle;
	// Use this for initialization
	void Start () {
        timer = 0;
        angle = 0;
        Random.InitState(42);
        spotLightParent = GameObject.Find("Spot Lights");
        spotLights = new List<Transform>();
        spotLights.Add(spotLightParent.transform.GetChild(0));
        spotLights.Add(spotLightParent.transform.GetChild(1));
        spotLights.Add(spotLightParent.transform.GetChild(2));
        spotLights.Add(spotLightParent.transform.GetChild(3));
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        angle = timer;
        spotLights[0].position = new Vector3(spotLights[0].position.x + Mathf.Sin(angle) / (2 * Mathf.PI), spotLights[0].position.y, spotLights[0].position.z - Mathf.Cos(angle) / (2 * Mathf.PI));
        spotLights[1].position = new Vector3(spotLights[1].position.x - Mathf.Sin(angle) / (2 * Mathf.PI), spotLights[1].position.y, spotLights[1].position.z + Mathf.Cos(angle) / (2 * Mathf.PI));
        spotLights[2].position = new Vector3(spotLights[2].position.x + Mathf.Sin(angle) / (2 * Mathf.PI), spotLights[2].position.y, spotLights[2].position.z + Mathf.Cos(angle) / (2 * Mathf.PI));
        spotLights[3].position = new Vector3(spotLights[3].position.x - Mathf.Sin(angle) / (2 * Mathf.PI), spotLights[3].position.y, spotLights[3].position.z - Mathf.Cos(angle) / (2 * Mathf.PI));
	}
}
