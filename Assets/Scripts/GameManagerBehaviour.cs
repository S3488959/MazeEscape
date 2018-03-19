using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerBehaviour : NetworkManager {

    List<SlaveBehaviour> slaves = new List<SlaveBehaviour>();
    Transform spawnPoints;

    // Use this for initialization
    void Start() {
	}

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        GameObject player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        slaves.Add(player.GetComponent<SlaveBehaviour>());
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    // Update is called once per frame
    void Update () {
	}

    public SlaveBehaviour[] GetSlaves() {
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
