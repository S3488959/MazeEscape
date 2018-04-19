using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRaycastCheck : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
            return;

        Ray ray = GameObject.FindGameObjectWithTag("SlaveCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(ray.origin, ray.direction);

        RaycastHit[] hits = Physics.RaycastAll(ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Activatable act = hit.transform.GetComponent<Activatable>();
            if (act != null) {
                if (Input.GetKeyDown(KeyCode.E))
                    act.Activate();
            }
        }
		
	}
}
