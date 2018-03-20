﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerBehaviour : NetworkManager {

    List<PlayerBehaviour> slaves = new List<PlayerBehaviour>();
    Transform spawnPoints;

    // Use this for initialization
    void Start() {
	}

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        slaves.Add(player.GetComponent<PlayerBehaviour>());
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Update is called once per frame
    void Update () {
	}

    public PlayerBehaviour[] GetSlaves() {
        return slaves.ToArray();
    }

    public void Host() {
        StartHost();

    }

    public void ConnectAsPlayer() {
        StartClient();
    }

    public void ConnectAsSpectator() {
        StartClient();
    }

}
