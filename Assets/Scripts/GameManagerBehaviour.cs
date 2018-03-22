using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerBehaviour : NetworkManager {

    List<PlayerBehaviour> slaves = new List<PlayerBehaviour>();
    Transform spawnPoints;
    GameObject player;

    GameNetworkEvents networkEvents;

    GameVariables gameVars;


    // Use this for initialization
    void Start() {
        networkEvents = transform.GetChild(0).GetComponent<GameNetworkEvents>();
        gameVars = transform.GetChild(0).GetComponent<GameVariables>();
	}

    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        player = Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
        slaves.Add(player.GetComponent<PlayerBehaviour>());
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Update is called once per frame
    void Update () {
        if (gameVars.GetTimeRatio() < 0) {
            networkEvents.CmdMasterWins();
        }

	}

    public PlayerBehaviour[] GetSlaves() {
        return slaves.ToArray();
    }

    public void Host() {
        //Reset the match size, in case a game has already ended.
        matchSize = 10;
        StartHost();
    }

    public void ConnectAsPlayer() {
        StartClient();
    }

    public void ConnectAsSpectator() {
        StartClient();
    }

    //Intialise the countdown for total disconnect.
    //Ensure that no new players can join.
    public void DisconnectHostCheck() {
        matchSize = (uint)numPlayers;
    }

    public void DisconnectHost() {
        StopHost();
    } 

    public void DisconnectClient() {
        StopClient();
    }

}
