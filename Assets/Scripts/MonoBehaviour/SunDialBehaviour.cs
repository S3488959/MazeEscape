using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunDialBehaviour : MonoBehaviour {

    Quaternion startRot;

	// Use this for initialization
	void Start () {
        startRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = startRot;
	}
}
