using UnityEngine.Networking;
using Prototype.NetworkLobby;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameNetworkEvents : NetworkBehaviour {

    //The delate if the master wins.
    public delegate void MasterWinsDelegate();
    //The synchonised event that happens when the master wins.
    [SyncEvent]
    public event MasterWinsDelegate EventMasterWins;

    //The attached GamManagerBehaviour
    LobbyManager lm;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        //Get the GameManagaer Behaviour
        lm = GameObject.FindGameObjectWithTag("LobbyManager").GetComponent<LobbyManager>();

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
        SceneManager.LoadScene("ResultsScreen");

    }

    private void MasterWinsClient() {
        SceneManager.LoadScene("ResultsScreen");

    }


}   
