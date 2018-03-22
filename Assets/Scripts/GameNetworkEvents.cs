using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameNetworkEvents : NetworkBehaviour {

    //The delate if the master wins.
    public delegate void MasterWinsDelegate();
    //The synchonised event that happens when the master wins.
    [SyncEvent]
    public event MasterWinsDelegate EventMasterWins;

    //The attached GamManagerBehaviour
    GameManagerBehaviour gmb;

	// Use this for initialization
	void Start () {

        //Get the GameManagaer Behaviour
        gmb = transform.parent.GetComponent<GameManagerBehaviour>();

        //For the server and the client a diffrent fucntion is called when the master wins.
        EventMasterWins += TestWin;
    }

    [Command]
    public void CmdMasterWins() {
        if (isServer)
            EventMasterWins();
    }

    private void TestWin() {
        if (isServer)
            MasterWinsServer();
        else
            MasterWinsClient();
    }

    private void MasterWinsServer() {
        gmb.DisconnectHostCheck();
    }

    private void MasterWinsClient() {
        gmb.DisconnectClient();
    }


}   
