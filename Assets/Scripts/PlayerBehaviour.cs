using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {
    GameObject[] slaveCameras;
    SlaveController slaveCont;
    [SyncVar]
    int ap = 0;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

	// Use this for initialization
	void Start () {

        if (isServer)
        {
            if(!GameSettings.isMasterCharOff)
            {
                GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
                GameSettings.GameStart();
                gameObject.SetActive(false);
                GameSettings.isMasterCharOff = true;
            }
            else
            {
                turnOffCamera();
            }
        }      
        else
        {
            slaveCont = gameObject.GetComponent<SlaveController>();
        }

    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;          
        }

        SlaveControls();
		
	}

    void SlaveControls() {
        slaveCont.GetInput();
    }

    void MasterControls()
    {
        
    }

    void turnOffCamera()
    {
        slaveCameras = GameObject.FindGameObjectsWithTag("SlaveCamera");
        foreach (GameObject cam in slaveCameras)
        {
            cam.SetActive(false);
        }
    }
}
