﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBehaviour : NetworkBehaviour {
    public GameObject Canvas;
    SlaveController slaveCont;
    
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

        if (isServer && isLocalPlayer)
        {
            GameSettings.PLAYSTATE = GameSettings.PLAY_STATE.MASTER;
            Destroy(gameObject);       
        }
        if(!isLocalPlayer)
        {
            transform.GetComponent<AudioListener>().enabled = false;
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

    void SlaveControls()
    {
        slaveCont.GetInput();
    }

    void MasterControls()
    {
        
    }

}
