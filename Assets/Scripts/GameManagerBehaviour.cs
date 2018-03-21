using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManagerBehaviour : NetworkManager {

    public GameObject masterPrefab;
    public GameObject slavePrefab;
    public GameObject minimapPrefab;

    List<PlayerBehaviour> slaves = new List<PlayerBehaviour>();
    Transform spawnPoints;
    GameObject player;
    bool isMaster;

    // Use this for initialization
    void Start() {
        isMaster = true;
	}

    

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        player = Instantiate(playerPrefab, Vector3.up, Quaternion.identity);
        if (isMaster)
        {
            //player.GetComponent<NetworkIdentity>().serverOnly = false;
            //player.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
            GameObject master = Instantiate(masterPrefab, Vector3.up, Quaternion.identity);
            master.transform.parent = player.transform;
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
            GameObject minimap = Instantiate(minimapPrefab, Vector3.up, Quaternion.identity);
            isMaster = false;
        }
        else
        {
           // player.GetComponent<NetworkIdentity>().localPlayerAuthority = false;
            //player.GetComponent<NetworkIdentity>().serverOnly = true;         
            GameObject slave = Instantiate(slavePrefab, Vector3.up, Quaternion.identity);
            slave.transform.parent = player.transform;
            slaves.Add(player.GetComponent<PlayerBehaviour>());
            NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
        }
        
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

    public void Disconnect() {
        //StopHost();
        //StopClient();
    }

}
