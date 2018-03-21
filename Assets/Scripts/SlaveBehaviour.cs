using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlaveBehaviour : NetworkBehaviour {
    [SyncVar]
    int ap = 0;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (!isLocalPlayer)
            return;

        if (isServer)
            MasterControls();
        else
            SlaveControls();
		
	}

    void SlaveControls() {
        if (Input.GetKey(KeyCode.W)) {
            transform.position += Vector3.forward * Time.deltaTime;
        }
    }

    void MasterControls() { }

}
