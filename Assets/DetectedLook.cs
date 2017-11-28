using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedLook : MonoBehaviour {
    List<Detection> lights;
    GameObject player;
	// Use this for initialization
	void Start () {
        lights = new List<Detection>(GameObject.Find("Spot Lights").GetComponentsInChildren<Detection>());
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		foreach(Detection d in lights)
        {
            if(d.detected)
            {
                transform.LookAt(player.transform, transform.up);
                transform.Rotate(new Vector3(0, -90, 0));
            }
        }
	}
}
