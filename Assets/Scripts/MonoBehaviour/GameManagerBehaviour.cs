using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

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

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        Vector3 spawnPos = Vector3.up;
        switch (slaves.Count)
        {
            case (2):
                spawnPos += Vector3.right * 9;
                break;
            case (3):
                spawnPos += Vector3.left * 9;
                break;
            case (4):
                spawnPos += Vector3.forward * 9;
                break;
            case (5):
                spawnPos += Vector3.back * 9;
                break;
            case (6):
                spawnPos += Vector3.right * 9 + Vector3.forward * 9;
                break;
            case (7):
                spawnPos += Vector3.right * 9 + Vector3.back * 9;
                break;
            case (8):
                spawnPos += Vector3.left * 9 + Vector3.forward * 9;
                break;
            case (9):
                spawnPos += Vector3.left * 9 + Vector3.back * 9;
                break;
            default:
                break;
        }
        player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
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

    public void SetNetworkAddress(string address) {
        networkAddress = address;
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
