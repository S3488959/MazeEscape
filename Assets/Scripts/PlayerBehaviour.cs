using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {
    [SyncVar]
    int ap = 0;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

	// Use this for initialization
	void Start () {
        if (isServer) {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.position = new Vector3(0, 40, 0);
        }
		
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
