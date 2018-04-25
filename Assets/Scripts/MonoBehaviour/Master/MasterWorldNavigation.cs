using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWorldNavigation : MonoBehaviour {

    private const float CAMMOVESPEED = 40;

    bool controller = true;

	// Use this for initialization
	void Start () {
		if (controller) {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

	}
	
	// Update is called once per frame
	void Update () {

        Vector3 pos = transform.position;
        pos.x += Input.GetAxis("Horizontal") * Time.deltaTime * CAMMOVESPEED;
        pos.z += Input.GetAxis("Vertical") * Time.deltaTime * CAMMOVESPEED;
        transform.position = pos;



    }
}
