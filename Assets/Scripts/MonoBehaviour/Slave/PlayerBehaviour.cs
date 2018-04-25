using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {
    GameObject[] slaveCameras;
    public GameObject Canvas;
    SlaveController slaveCont;
    [SyncVar]
    int ap = 0;
    
    [SyncVar]
    public GameObject minimapPosToken = null;

    public override void OnStartLocalPlayer()
    {
        if (!isServer)
        {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.PLAYER;
            slaveCont = gameObject.GetComponent<SlaveController>();
            slaveCont.ChangeView();
            Canvas.SetActive(true);
        }
    }

    // Use this for initialization
    void Start () {

        if (isServer)
        {
            if(!GameSettings.isMasterCharOff)
            {
                GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
                GameSettings.GameStart();
                Destroy(gameObject);
                GameSettings.isMasterCharOff = true;
            }
        }      
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            transform.GetComponent<AudioListener>().enabled = false;
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

    void TurnOffCamera()
    {
        slaveCameras = GameObject.FindGameObjectsWithTag("SlaveCamera");
        foreach (GameObject cam in slaveCameras)
        {
            cam.SetActive(false);
        }
    }
}
