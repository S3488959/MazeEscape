using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerRaycastCheck : NetworkBehaviour {

    private SlaveController cont;
    public float castDistance;

	// Use this for initialization
	void Start () {
        cont = GetComponent<SlaveController>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
            return;
        Vector3 rayPos = gameObject.transform.position + Vector3.up;
        //Ray ray = GameObject.FindGameObjectWithTag("SlaveCamera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        Ray ray = new Ray(rayPos, gameObject.transform.forward);
        Debug.DrawLine(rayPos, rayPos + (gameObject.transform.forward * castDistance), Color.blue);
        //RaycastHit[] hits = Physics.RaycastAll(ray);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 1, out hit, castDistance))
        {
            Activatable act = hit.transform.GetComponent<Activatable>();
            if (act != null) {
                if (Input.GetKeyDown(KeyCode.E))
                    act.Activate();
            }
        }
		
	}
}
