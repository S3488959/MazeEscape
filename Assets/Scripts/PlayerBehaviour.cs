using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {

    GameObject[] slaveCameras;
    SlaveController slave;
    [SyncVar]
    int ap = 0;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

	// Use this for initialization
	void Start () {
        
        if(isLocalPlayer)
            slave = gameObject.GetComponent<SlaveController>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;          
        }

        if (isServer)
            MasterControls();
        else
            SlaveControls();
		
	}

    void SlaveControls() {
        slave.GetInput();
        GameObject.FindGameObjectWithTag("MainCamera").SetActive(false);
    }

    void MasterControls()
    {
        slaveCameras = GameObject.FindGameObjectsWithTag("SlaveCamera");
        foreach(GameObject cam in slaveCameras)
        {
            cam.SetActive(false);
        }
    }

}
